namespace Marimba
{
    using System;
    using System.Data.SQLite;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using Marimba.Utility;

    /// <summary>
    /// The main Club object which holds all the information we want to keep track of
    /// </summary>
    class Club
    {
        /// <summary>
        /// the latest file version, and the version used to save
        /// </summary>
        public static readonly double FileVersion = 2.2;

        /// <summary>
        /// used to write the main file
        /// </summary>
        public BinaryReader br;

        /// <summary>
        /// used to write the main file
        /// </summary>
        public static BinaryWriter bw;

        /// <summary>
        /// used to write the main file
        /// </summary>
        public FileStream fs;

        /// <summary>
        /// aesInfo is for encryption, AES encryption; stores encryption information
        /// </summary>
        private Aes aesInfo;

        /// <summary>
        /// this essentially contains the encrypted part of the club... it should be cleared once no longer needed (since it is huge)
        /// </summary>
        private byte[] cipherText;

        /// <summary>
        /// strName is the name of the club
        /// </summary>
        public string strName;

        /// <summary>
        /// fileVersion contains the version of the file currently loaded
        /// </summary>
        public double fileVersion;

        //I do not recommend publicly releasing any .mrb files and use a unique password for Marimba
        public List<User> strUsers;
        private static readonly string[] priviledges = { "Exec", "Admin" };
        private static readonly char[] allowedCharacters = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        /// <summary>
        /// Number of members currently in the club
        /// </summary>
        public short iMember;

        /// <summary>
        /// array of members on the mailing list
        /// currently, prepare for five thousand total members
        /// </summary>
        public Member[] members = new Member[5000];

        /// <summary>
        /// location of the .mrb file to save this club to
        /// </summary>
        protected string strLocation;

        /// <summary>
        /// Name of current logged in user
        /// </summary>
        public string strCurrentUser;
        
        /// <summary>
        /// Privilege level of current logged in user
        /// </summary>
        public string strCurrentUserPrivilege;

        /// <summary>
        /// listTerms contains all of the terms in the loaded Marimba file
        /// </summary>
        public List<Term> listTerms;

        /// <summary>
        /// TODO: remove this
        /// </summary>
        public Election currentElection;

        /// <summary>
        /// list of budget items stored in Marimba
        /// </summary>
        public List<BudgetItem> budget;

        /// <summary>
        /// list of history items stored in Marimba
        /// </summary>
        public List<HistoryItem> historyList;

        /// <summary>
        /// Email address for the club
        /// </summary>
        public string emailAddress;

        /// <summary>
        /// IMAP server address for the club email
        /// </summary>
        public string imapServerAddress;

        /// <summary>
        /// SMTP server address for the club email
        /// </summary>
        public string smptServerAddress;
        
        /// <summary>
        /// Whether SSL is required for the club's IMAP server
        /// </summary>
        public bool bImap;

        /// <summary>
        /// Whether SSL is required for the club's SMTP server
        /// </summary>
        public bool imapRequiresSSL;

        /// <summary>
        /// The SMTP server's port to use
        /// </summary>
        public int smtpRequiresSSL;

        /// <summary>
        /// The email object that handles the email functions in Marimba
        /// </summary>
        public Email clubEmail;

        /// <summary>
        /// The length of the salt to use for passwords
        /// </summary>
        private static readonly int SaltLength = 16;

        /// <summary>
        /// when initializing the list of budget items, how much buffer to add at the end of the list for new budget items
        /// </summary>
        private static readonly int BudgetBuffer = 50;

        /// <summary>
        /// when initializing the list of history items, how much buffer to add at the end of the list for new budget items
        /// </summary>
        private static readonly int HistoryBuffer = 50;

        /// <summary>
        /// The possible user privileges in Marimba
        /// </summary>
        private static readonly string[] ValidPrivileges = { "Exec", "Admin" };

        public Club(string strLocation, Aes aesKey = null)
        {
            this.strLocation = strLocation;
            this.strName = String.Empty;
            this.aesInfo = aesKey;
        }

        /// <summary>
        /// Used for duplicating the club, specifically for Excel
        /// </summary>
        /// <param name="strLocation">The file location to save the new club to</param>
        /// <returns>A clone of the current club</returns>
        public Club CloneClub(string strLocation)
        {
            return new Club(this.strLocation, this.aesInfo);
        }

        /// <summary>
        /// Loads the unencrypted part of the .mrb file into the Club
        /// </summary>
        public void LoadClub()
        {
            // read the given file, update all of the appropriate information
            this.fs = new FileStream(this.strLocation, FileMode.Open);

            this.br = new BinaryReader(fs);
            fileVersion = this.br.ReadDouble(); // read the version number, needed for reading legacy file formats
            this.strName = this.br.ReadString();
            int iUser = this.br.ReadInt16();

            // this next part is for importing old files
            int numUsers;
            if (fileVersion < 2)
                numUsers = 20;
            else
                numUsers = iUser;

            strUsers = new List<User>(iUser);
            for (int i = 0; i < numUsers; i++)
            {
                string[] userFields = new string[User.numUserFields];
                for (int j = 0; j < User.numUserFields; j++)
                {
                    userFields[j] = this.br.ReadString();
                }
                User user = new User
                {
                    name = userFields[0],
                    saltAndPassword = userFields[1],
                    priviledge = userFields[2],
                    keyXORPassword = userFields[3]
                };
                strUsers.Add(user);
            }
            
            // string strKey = br.ReadString();
            string strIV = this.br.ReadString();

            // REMOVE LATER: Unsafe, only suitable for testing
            this.aesInfo = Aes.Create();

            // aesInfo.Key = Convert.FromBase64String(strKey);
            this.aesInfo.IV = Convert.FromBase64String(strIV);

            cipherText = this.br.ReadBytes(Convert.ToInt32(this.br.BaseStream.Length - this.br.BaseStream.Position));
        }

        /// <summary>
        /// Loads the encrypted part of the .mrb file into the Club
        /// </summary>
        public void LoadEncryptedSection()
        {
            /**********************************
             * HOW ENCRYPTION IN MARIMBA WORKS*
             * One part of the .mrb file is encrypted, the other is not
             * The part that is not encrypted is login information
             * A user will try to login with their password
             * The double-hash of the password is used to check the hash is correct
             * If correct, login will retrieve the key the rest of the file is encrypted with
             * That key is stored by being xor'd with the single hash of the password
             * As long as the password is unknown and SHA-256 is preimage resistant, this should be secure
             * The rest of the file is encrypted in 256-bit AES
             * The key is retreived, and the rest of the file is decrypted
             * The file is loaded into Marimba as normally would be
             * If a user changes their password, the key xor'd with the single hash is redone
             * If an admin updates the key, everyone gets their key/hash thing updated
             * This can easily be done if the key is known since xor is linear
             * */

            // DECRYPT ENCRYPTED SECTION
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = this.aesInfo.Key;
                aesAlg.IV = this.aesInfo.IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cryptoStream))
                        {
                            // read the membership list
                            iMember = Convert.ToInt16(reader.ReadLine());
                            for (int i = 0; i < iMember; i++)
                                members[i] = new Member(reader);
                            short numTerms = Convert.ToInt16(reader.ReadLine());
                            listTerms = new List<Term>(numTerms);
                            for (int i = 0; i < numTerms; i++)
                            {
                                Term nextTerm = new Term(reader);
                                listTerms.Add(nextTerm);
                            }
                            
                            // read the budget stuff
                            int iBudget = Convert.ToInt32(reader.ReadLine());
                            this.budget = new List<BudgetItem>(iBudget + BudgetBuffer);
                            List<int> assetToDepIndices = new List<int>(iBudget);
                            for (int i = 0; i < iBudget; i++)
                            {
                                BudgetItem newItem = new BudgetItem();
                                newItem.value = Convert.ToDouble(reader.ReadLine());
                                newItem.name = reader.ReadLine();
                                newItem.dateOccur = new DateTime(Convert.ToInt64(reader.ReadLine()));
                                newItem.dateAccount = new DateTime(Convert.ToInt64(reader.ReadLine()));
                                newItem.cat = reader.ReadLine();
                                newItem.type = (TransactionType)Convert.ToInt32(reader.ReadLine());
                                newItem.term = Convert.ToInt32(reader.ReadLine());
                                newItem.comment = ClsStorage.ReverseCleanNewLine(reader.ReadLine());
                                budget.Add(newItem);

                                assetToDepIndices.Add(Convert.ToInt32(reader.ReadLine()));
                            }

                            int k = 0;
                            foreach (BudgetItem item in this.budget)
                            {
                                int assetToDepIndex = assetToDepIndices[k];
                                item.depOfAsset = (assetToDepIndex == -1) ? null : this.budget[assetToDepIndex];
                                k++;
                            }

                            int iHistory = Convert.ToInt32(reader.ReadLine());
                            historyList = new List<HistoryItem>(iHistory + HistoryBuffer);
                            for (int i = 0; i < iHistory; i++)
                            {
                                HistoryItem nextItem = new HistoryItem(reader);
                                historyList.Add(nextItem);
                            }

                            // read email
                            if (fileVersion >= 2)
                            {
                                emailAddress = reader.ReadLine();
                                imapServerAddress = reader.ReadLine();
                                this.bImap = Convert.ToBoolean(reader.ReadLine());
                                smptServerAddress = reader.ReadLine();
                                smtpRequiresSSL = Convert.ToInt32(reader.ReadLine());
                                imapRequiresSSL = Convert.ToBoolean(reader.ReadLine());
                            }
                        }
                    }
                }

                // remove cipherText from storage
                cipherText = null;
            }

            this.clubEmail = new Email(emailAddress, Properties.Settings.Default.emailPassword, imapServerAddress, this.bImap, smptServerAddress, smtpRequiresSSL, imapRequiresSSL);
        }

        /// <summary>
        /// Exports the current club to the new file version format.
        /// </summary>
        /// <param name="destinationFilePath"></param>
        /// <param name="worker"></param>
        public void ExportSqlLiteClub(string destinationFilePath, BackgroundWorker worker)
        {
            string usersDbFilename = Path.ChangeExtension(Path.GetRandomFileName(), "db");
            string mainDbFilename = Path.ChangeExtension(Path.GetRandomFileName(), "db");

            string usersDbPath = Path.Combine(Path.GetTempPath(), usersDbFilename);
            string mainDbPath = Path.Combine(Path.GetTempPath(), mainDbFilename);
            
            // export user table
            var userDbConnectionString = new SQLiteConnectionStringBuilder { DataSource = usersDbPath }.ToString();
            using (var connection = new SQLiteConnection(userDbConnectionString))
            {
                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE user
                        (name VARCHAR(255) NOT NULL,
                        salt BLOB NOT NULL,
                        password VARCHAR(255) NOT NULL,
                        priviledge INTEGER NOT NULL,
                        key_xor_password VARCHAR(255) NOT NULL);",
                    new List<string> { },
                    new List<string> { "@name", "@salt", "@password", "@priviledge", "@key_xor_password" },
                    "INSERT INTO user VALUES (@name, @salt, @password, @priviledge, @key_xor_password);",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        foreach (User user in strUsers)
                        {
                            string[] parsedPassword = user.saltAndPassword.Split('$');

                            parameters[0].Value = user.name;
                            parameters[1].Value = StringToByteArray(parsedPassword[0]);
                            parameters[2].Value = parsedPassword[1];
                            parameters[3].Value = Array.IndexOf(ValidPrivileges, user.priviledge);
                            parameters[4].Value = user.keyXORPassword;

                            tasks.Add(command.ExecuteNonQueryAsync());
                        }
                        return tasks;
                    }
                );
            }

            // export main table
            var mainDbConnectionString = new SQLiteConnectionStringBuilder { DataSource = mainDbPath }.ToString();
            using (var connection = new SQLiteConnection(mainDbConnectionString))
            {
                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE member
                        (rowid INTEGER NOT NULL,
                        first_name VARCHAR(255) NOT NULL, last_name VARCHAR(255) NOT NULL, other_instrument_name VARCHAR(255),
                        email VARCHAR(255) NOT NULL, member_type INTEGER NOT NULL, faculty INTEGER NOT NULL,
                        student_number VARCHAR(255) NOT NULL, signup_time DATETIME NOT NULL, shirt_size INTEGER NOT NULL,
                        PRIMARY KEY(rowid));",
                    new List<string> { },
                    new List<string> {
                        "@first_name", "@last_name", "@other_instrument_name",
                        "@email", "@member_type", "@faculty",
                        "@student_number", "@signup_time", "@shirt_size"
                    },
                    @"INSERT INTO member
                        (first_name, last_name, other_instrument_name, email, member_type, faculty, student_number, signup_time, shirt_size) VALUES
                        (@first_name, @last_name, @other_instrument_name, @email, @member_type, @faculty, @student_number, @signup_time, @shirt_size);",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        foreach (Member member in members) {
                            parameters[0].Value = member.firstName;
                            parameters[1].Value = member.lastName;
                            parameters[2].Value = String.IsNullOrWhiteSpace(member.otherInstrument) ? null : member.otherInstrument;
                            parameters[3].Value = member.email;
                            parameters[4].Value = (int)member.type;
                            parameters[5].Value = (int)member.memberFaculty;
                            parameters[6].Value = member.uiStudentNumber.ToString();
                            parameters[7].Value = member.signupTime;
                            parameters[8].Value = (int)member.size;
                            tasks.Add(command.ExecuteNonQueryAsync());
                        }
                        return tasks;
                    }
                );

                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE member_instruments
                        (member INTEGER NOT NULL, relationship INTEGER NOT NULL, instrument INTEGER NOT NULL,
                        FOREIGN KEY(member) REFERENCES member(rowid));",
                    new List<string> {
                        @"CREATE INDEX member_relationship_on_member_instruments ON member_instruments (member, relationship);"
                    },
                    new List<string> {
                        "@member", "@instrument", "@relationship"
                    },
                    "INSERT INTO member_instruments VALUES (@member, @instrument, @relationship)",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        foreach (Member member in members)
                        {
                            int indexOfPrimaryInstrument = (int)member.curInstrument;
                            parameters[0].Value = Array.IndexOf(members, member) + 1;
                            if (member.playsInstrument == null)
                            {
                                parameters[1].Value = indexOfPrimaryInstrument;
                                parameters[2].Value = 0; // primary instrument
                                tasks.Add(command.ExecuteNonQueryAsync());
                                continue;
                            }

                            for (int i = 0; i < member.playsInstrument.Length; i++)
                            {
                                if (!member.playsInstrument[i]) continue;

                                parameters[1].Value = i;
                                parameters[2].Value = (i == indexOfPrimaryInstrument) ? 0 : 1;
                                tasks.Add(command.ExecuteNonQueryAsync());
                            }
                        }
                        return tasks;
                    }
                );

                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE term 
                        (rowid INTEGER NOT NULL,
                        name VARCHAR(255) NOT NULL,
                        start_date DATETIME NOT NULL,
                        end_date DATETIME NOT NULL,
                        PRIMARY KEY(rowid));",
                    new List<string> { },
                    new List<string> { "@name", "@start_date", "@end_date" },
                    "INSERT INTO term (name, start_date, end_date) VALUES (@name, @start_date, @end_date);",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        foreach (Term term in listTerms)
                        {
                            parameters[0].Value = term.strName;
                            parameters[1].Value = term.startDate;
                            parameters[2].Value = term.endDate;
                            tasks.Add(command.ExecuteNonQueryAsync());
                        }
                        return tasks;
                    }
                );

                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE rehearsal_date
                        (rowid INTEGER NOT NULL, term INTEGER NOT NULL, date DATE NOT NULL,
                        PRIMARY KEY (rowid),
                        FOREIGN KEY (term) REFERENCES term(rowid));",
                    new List<string> {
                        "CREATE INDEX term_on_term_rehearsal_dates ON rehearsal_date (term)",
                        "CREATE UNIQUE INDEX date_on_term_rehearsal_dates ON rehearsal_date (date)"
                    },
                    new List<string> { "@term", "@date" },
                    "INSERT INTO rehearsal_date (term, date) VALUES (@term, @date);",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        for (int i = 0; i < listTerms.Count(); i++)
                        {
                            parameters[0].Value = i + 1; // again, +1 because term index starts at 1
                            foreach (DateTime date in listTerms[i].rehearsalDates)
                            {
                                parameters[1].Value = date;
                                tasks.Add(command.ExecuteNonQueryAsync());
                            }
                        }
                        return tasks;
                    }
                );

                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE term_member
                        (term INTEGER NOT NULL, member INTEGER NOT NULL,
                        FOREIGN KEY (term) REFERENCES term(rowid),
                        FOREIGN KEY (member) REFERENCES member(rowid));",
                    new List<string>
                    {
                        "CREATE INDEX term_on_term_member ON term_member (term)",
                        "CREATE INDEX member_on_term_member ON term_member (member)"
                    },
                    new List<string> { "@term", "@member" },
                    "INSERT INTO term_member VALUES (@term, @member);",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        for (int i = 0; i < listTerms.Count(); i++)
                        {
                            parameters[0].Value = i + 1; // again, +1 because term index starts at 1

                            Term currentTerm = listTerms[i];
                            foreach (short sID in currentTerm.members)
                            {
                                int memberIndex = ClsStorage.currentClub.FindMember(sID);
                                parameters[1].Value = memberIndex + 1;
                                tasks.Add(command.ExecuteNonQueryAsync());
                            }
                        }

                        return tasks;
                    }
                );

                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE rehearsal_attendance
                        (date INTEGER NOT NULL, member INTEGER NOT NULL,
                        FOREIGN KEY (date) REFERENCES rehearsal_date(rowid),
                        FOREIGN KEY (member) REFERENCES member(rowid));",
                    new List<string>
                    {
                        "CREATE INDEX date_on_rehearsal_attendance ON rehearsal_attendance (date)",
                        "CREATE INDEX member_on_rehearsal_attendance ON rehearsal_attendance (member)"
                    },
                    new List<string> { "@date", "@member" },
                    "INSERT INTO rehearsal_attendance VALUES (@date, @member);",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        for (int i = 0; i < listTerms.Count(); i++)
                        {
                            // TODO
                        }

                        return tasks;
                    }
                );

                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE term_fees
                        (rowid INTEGER NOT NULL, term INTEGER NOT NULL, name VARCHAR(255) NOT NULL, amount DOUBLE NOT NULL,
                        PRIMARY KEY (rowid),
                        FOREIGN KEY (term) REFERENCES term(rowid));",
                    new List<string>
                    {
                        "CREATE INDEX term_on_other_term_fees ON other_term_fees (term)"
                    },
                    new List<string> { "@term", "@name", "@amount" },
                    "INSERT INTO other_term_fees (term, name, amount) VALUES (@term, @name, @amount);",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        for (int i = 0; i < listTerms.Count(); i++)
                        {
                            parameters[0].Value = i + 1; // again, +1 because term index starts at 1
                            
                            Term currentTerm = listTerms[i];

                            parameters[1].Value = "Membership Fee";
                            parameters[2].Value = currentTerm.membershipFees;
                            tasks.Add(command.ExecuteNonQueryAsync());

                            for (int j = 0; j < currentTerm.otherFeesNames.Length; j++)
                            {
                                parameters[1].Value = currentTerm.otherFeesNames[j];
                                parameters[2].Value = currentTerm.otherFeesAmounts[j];
                                tasks.Add(command.ExecuteNonQueryAsync());
                            }
                        }

                        return tasks;
                    }
                );

                CreateAndPopulateTable(
                    connection,
                    @"CREATE TABLE fee_payments
                        (term INTEGER NOT NULL, member INTEGER NOT NULL, fee INTEGER NOT NULL, amount_paid DOUBLE NOT NULL, date_paid DATETIME NOT NULL,
                        FOREIGN KEY (fee) REFERENCES term_fees(rowid),
                        FOREIGN KEY (term) REFERENCES term(rowid),
                        FOREIGN KEY (member) REFERENCES member(rowid));",
                    new List<string>
                    {
                        "CREATE INDEX member_on_fee_payments ON fee_payments (member)",
                        "CREATE INDEX term_member_on_fee_payments ON fee_payments (term, member)"
                    },
                    new List<string> { "@term", "@member", "@fee", "@amount_paid", "@date_paid" },
                    "INSERT INTO fee_payments VALUES (@term, @member, @fee, @amount_paid, @date_paid);",
                    (command, parameters) =>
                    {
                        var tasks = new List<Task<int>>();
                        for (int i = 0; i < listTerms.Count(); i++)
                        {
                            parameters[0].Value = i + 1; // again, +1 because term index starts at 1

                            Term currentTerm = listTerms[i];

                            for (int j = 0; j < currentTerm.members.Length; j++)
                            {
                                short sID = currentTerm.members[j];
                                int memberIndex = ClsStorage.currentClub.FindMember(sID);

                                parameters[1].Value = memberIndex + 1;
                                
                                double membershipFeePaid = currentTerm.feesPaid[memberIndex, 0];
                                if (membershipFeePaid > 0.01)
                                {

                                    parameters[2].Value = (int)GetTermFeeID(connection, i + 1, "Membership Fee");
                                    parameters[3].Value = membershipFeePaid;
                                    parameters[4].Value = currentTerm.feesPaidDate[memberIndex, 0];
                                    tasks.Add(command.ExecuteNonQueryAsync());
                                }

                                // TODO: add other fee payments
                            }
                        }

                        return tasks;
                    }
);
            }
        }

        /// <summary>
        /// A lambda function to create a table in the given SQLite connection.
        /// </summary>
        /// <param name="connection">An open connection to the SQLite Database.</param>
        /// <param name="createCommand">The string SQLite command to create the table.</param>
        /// <param name="indexCommands">A list of string SQLite commands to create indexes on the table.</param>
        /// <param name="insertParameterNames">The names of the parameters to use in the prepared insert statement.</param>
        /// <param name="insertCommand">The string SQLite commmand to insert into the table using the parameter names in insertParameterNames.</param>
        /// <param name="createInsertTasks">A function which consumes the insert parameters and returns an enumerable of Tasks which will insert a row into the table being created.</param>
        protected void CreateAndPopulateTable(
            SQLiteConnection connection,
            string createCommand,
            IList<string> indexCommands,
            IList<string> insertParameterNames,
            string insertCommand,
            Func<SQLiteCommand, IList<SQLiteParameter>, IList<Task<int>>> createInsertTasks)
        {
            using (var command = new SQLiteCommand(connection))
            {
                using (var tx = connection.BeginTransaction())
                {
                    command.CommandText = createCommand;
                    command.ExecuteNonQuery();

                    foreach (string indexCommand in indexCommands)
                    {
                        command.CommandText = indexCommand;
                        command.ExecuteNonQuery();
                    }

                    command.CommandText = insertCommand;
                    var parameters = insertParameterNames.Select(parameterName => new SQLiteParameter(parameterName)).ToList();
                    command.Parameters.AddRange(parameters.ToArray());

                    var insertTasks = createInsertTasks(command, parameters);
                    Task.WaitAll(insertTasks.ToArray());
                    tx.Commit();
                }
            }
        }

        /// <summary>
        /// Gets the rowid of the input fee from the input database.
        /// </summary>
        /// <param name="connection">An open connetion to the SQLite Database.</param>
        /// <param name="termID">The term of the fee.</param>
        /// <param name="feeName">The name of the fee.</param>
        protected object GetTermFeeID(SQLiteConnection connection, int termID, string feeName)
        {
            using (var queryCommand = new SQLiteCommand(connection))
            {
                queryCommand.CommandText = "SELECT rowid FROM term_fees WHERE term_fees.term = @term AND term_fees.name = @feeName";
                var termParameter = new SQLiteParameter("@term", termID);
                queryCommand.Parameters.Add(termParameter);

                var nameParameter = new SQLiteParameter("@feeName", feeName);
                queryCommand.Parameters.Add(nameParameter);

                return queryCommand.ExecuteScalar();
            }
        }

        /// <summary>
        /// Saves the current club
        /// </summary>
        public void SaveClub()
        {
            if (this.br != null)
                this.br.Close();
            fs = new FileStream(this.strLocation, FileMode.Create);
            bw = new BinaryWriter(fs);

            // this line is the file version number
            // this will be useful later on if .mrb files are siginificantly modified
            bw.Write(FileVersion);
            bw.Write(strName);
            bw.Write((short)strUsers.Count);

            // write the users (i.e. exec account information)
            foreach (User user in strUsers)
            {
                bw.Write(user.name);
                bw.Write(user.saltAndPassword);
                bw.Write(user.priviledge);
                bw.Write(user.keyXORPassword);
            }
            
            // ENCRYPTED SECTION
            byte[] bEncryptedSection;
            
            // generate a new IV
            this.aesInfo.GenerateIV();
            bw.Write(Convert.ToBase64String(this.aesInfo.IV));

            // In future, set key to whatever here
            // aesAlg.Key;
            // create encryptor
            using (Aes AesEncrypt = Aes.Create())
            {
                AesEncrypt.Key = this.aesInfo.Key;
                AesEncrypt.IV = this.aesInfo.IV;
                ICryptoTransform encryptor = AesEncrypt.CreateEncryptor(AesEncrypt.Key, AesEncrypt.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            // write the members (i.e. mailing/membership list)
                            sw.WriteLine(iMember);
                            for (int i = 0; i < iMember; i++)
                            {
                                sw.WriteLine(members[i].firstName);
                                sw.WriteLine(members[i].lastName);
                                sw.WriteLine((int)members[i].type);
                                sw.WriteLine(members[i].uiStudentNumber);
                                sw.WriteLine((int)members[i].memberFaculty);

                                // if the member plays an "other" instrument, write it here
                                // write blank if the member does not play an other instrument
                                sw.WriteLine(members[i].otherInstrument);

                                // write the main instrument
                                sw.WriteLine((int)members[i].curInstrument);

                                // write if the member plays multiple instruments
                                // write any other instruments that the member plays (or does not play)
                                sw.WriteLine(members[i].bMultipleInstruments);
                                int numberOfInstruments = Enum.GetValues(typeof(Member.Instrument)).Length;
                                if (members[i].bMultipleInstruments)
                                    for (int j = 0; j < numberOfInstruments; j++)
                                        sw.WriteLine(members[i].playsInstrument[j]);

                                sw.WriteLine(members[i].email);
                                sw.WriteLine(ClsStorage.CleanNewLine(members[i].comments));
                                sw.WriteLine(members[i].sID);
                                sw.WriteLine(members[i].signupTime.Ticks);
                                sw.WriteLine((int)members[i].size);
                            }

                            // write the terms
                            sw.WriteLine(listTerms.Count);

                            // loop through the terms
                            foreach (Term currentTerm in listTerms)
                            {
                                currentTerm.saveTerm(sw);
                            }

                            // save the budget
                            sw.WriteLine(this.budget.Count);
                            foreach (BudgetItem item in this.budget)
                            {
                                sw.WriteLine(item.value);
                                sw.WriteLine(item.name);
                                sw.WriteLine(item.dateOccur.Ticks);
                                sw.WriteLine(item.dateAccount.Ticks);
                                sw.WriteLine(item.cat);
                                sw.WriteLine((int)item.type);
                                sw.WriteLine(item.term);
                                sw.WriteLine(ClsStorage.CleanNewLine(item.comment));
                                sw.WriteLine(this.budget.IndexOf(item.depOfAsset));
                            }

                            // save history
                            sw.WriteLine(historyList.Count);
                            foreach (HistoryItem item in historyList)
                            {
                                item.saveHistory(sw);
                            }
                            
                            // save email details
                            sw.WriteLine(emailAddress);
                            sw.WriteLine(imapServerAddress);
                            sw.WriteLine(this.bImap);
                            sw.WriteLine(smptServerAddress);
                            sw.WriteLine(smtpRequiresSSL);
                            sw.WriteLine(imapRequiresSSL);
                        }

                        bEncryptedSection = ms.ToArray();
                        bw.Write(bEncryptedSection);
                    }
                }
            }

            bw.Close();
            fs.Close();
            
            // reopen the binary reader to prevent anyone else from editing the file
            this.br = new BinaryReader(new FileStream(this.strLocation, FileMode.Open));
        }

        /// <summary>
        /// Saves the club to the given file location
        /// </summary>
        /// <param name="strLocation">Location of .mrb file to save to</param>
        public void SaveClub(string strLocation)
        {
            // change the location, and then save
            this.strLocation = strLocation;
            SaveClub();
        }

        /// <summary>
        /// add this user with the given name, password and privilege level
        /// </summary>
        /// <param name="strName">Name of user</param>
        /// <param name="strPassword">Password selected by user</param>
        /// <param name="strPrivileges">Privilege level for user</param>
        /// <returns>Whether user creation was successful</returns>
        public bool AddUser(string strName, string strPassword, string strPrivileges)
        {
            // see if a user with this name already exists
            if (FindUser(strName) != null)
                return false;

            // priviledge level isn't allowed
            if (Array.IndexOf(ValidPrivileges, strPrivileges) < 0)
            {
                return false;
            }

            // do a basic encryption on the password
            // the intention is just so that no one can read plaintext passwords
            // I am well aware this algorithm isn't particularly strong, but it is sufficient for our needs
            SHA256 shaHash = SHA256.Create();

            // salt
            byte[] salt = new byte[SaltLength];
            int passwordLength = Encoding.UTF8.GetBytes(strPassword).Length;
            byte[] saltPlusPassword = new byte[SaltLength + passwordLength];

            // generate salt
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(salt);
            }

            // combine the salt and password
            Array.Copy(salt, saltPlusPassword, SaltLength);
            Array.Copy(Encoding.UTF8.GetBytes(strPassword), 0, saltPlusPassword, SaltLength, passwordLength);

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

            User newUser = new User
            {
                name = strName,
                saltAndPassword = ConvertToString(salt) + "$" + ConvertToString(data),
                priviledge = strPrivileges,
                keyXORPassword = Convert.ToBase64String(ClsStorage.XOR(this.aesInfo.Key, shaHash.ComputeHash(saltPlusPassword)))
            };
            strUsers.Add(newUser);
            return true;
        }

        /// <summary>
        /// remove the user with the given name from Marimba
        /// </summary>
        /// <param name="strName">The name of the user to remove</param>
        /// <returns>Whether removal was successful</returns>
        public bool DeleteUser(string strName)
        {
            User user = FindUser(strName);
            if (user == null)
            {
                return false;
            }

            return strUsers.Remove(user);
        }

        /// <summary>
        /// get the specified user
        /// </summary>
        /// <param name="strName">name of user</param>
        /// <returns>User object if there is a user with the given name, null if doesn't exist</returns>
        public User FindUser(string strName)
        {
            // first, find the user
            foreach (User user in strUsers)
            {
                if (strName == user.name)
                    return user;
            }

            // did not find user
            return null;
        }

        /// <summary>
        /// returns true if the login was successful, false otherwise
        /// </summary>
        /// <param name="strName">name of user to login as</param>
        /// <param name="strPassword">password to user to login as</param>
        /// <returns>Whether the login was successful</returns>
        public bool LoginUser(string strName, string strPassword)
        {
            User user = FindUser(strName);
            if (user == null)
                return false;
            else
            {
                // create a hash of the password and compare
                SHA256 shaHash = SHA256.Create();

                // Convert the input string to a byte array and compute the hash.

                // retrieve the salt and hash
                string[] parsedPassword = user.saltAndPassword.Split('$');
                byte[] salt = StringToByteArray(parsedPassword[0]);
                string hash = parsedPassword[1];

                // calculate hash of salt + password
                int passwordLength = Encoding.UTF8.GetBytes(strPassword).Length;
                byte[] saltPlusPassword = new byte[SaltLength + passwordLength];
                Array.Copy(salt, saltPlusPassword, SaltLength);
                Array.Copy(Encoding.UTF8.GetBytes(strPassword), 0, saltPlusPassword, SaltLength, passwordLength);

                byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                if (StringComparer.OrdinalIgnoreCase.Compare(hash, ConvertToString(data)) == 0)
                {
                    this.strCurrentUser = strName;
                    this.strCurrentUserPrivilege = user.priviledge;
                    try
                    {
                        this.aesInfo.Key = ClsStorage.XOR(Convert.FromBase64String(user.keyXORPassword), shaHash.ComputeHash(saltPlusPassword));
                    }
                    catch
                    {
                        if (Properties.Settings.Default.playSounds)
                            Sound.Error.Play();
                        System.Windows.Forms.MessageBox.Show("Username and password are correct, but key is corrupted. Unable to open file.");
                        return false;
                    }

                    return true;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Edit the currently logged in user
        /// </summary>
        /// <param name="strName">Name of the current user</param>
        /// <param name="strPassword">Old password of the current user</param>
        /// <param name="strNewPassword">New password of the current user</param>
        /// <returns>Whether the current user was successfully edited</returns>
        public bool EditUser(string strName, string strPassword, string strNewPassword)
        {
            // check user exists and current password is correct
            if (LoginUser(strName, strPassword))
            {
                User user = FindUser(strName);

                // replace the old password with the new password
                SHA256 shaHash = SHA256.Create();

                // salt
                byte[] salt = new byte[SaltLength];
                int passwordLength = Encoding.UTF8.GetBytes(strNewPassword).Length;
                byte[] saltPlusPassword = new byte[SaltLength + passwordLength];

                // generate salt
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetBytes(salt);
                }

                // combine the salt and password
                Array.Copy(salt, saltPlusPassword, SaltLength);
                Array.Copy(Encoding.UTF8.GetBytes(strNewPassword), 0, saltPlusPassword, SaltLength, passwordLength);

                // Convert the input string to a byte array and compute the hash.
                byte[] data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));
                user.saltAndPassword = ConvertToString(salt) + "$" + ConvertToString(data);

                // add the key used to encrypted the files here
                user.keyXORPassword = Convert.ToBase64String(ClsStorage.XOR(this.aesInfo.Key, shaHash.ComputeHash(saltPlusPassword)));

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Changes the privilege level of this user
        /// </summary>
        /// <param name="strName">Name of the user to edit</param>
        /// <param name="strNewPrivilege">New privilege level for user</param>
        /// <returns>Whether editing privilege was successful</returns>
        public bool EditUserPrivilege(string strName, string strNewPrivilege)
        {
            User user = FindUser(strName);
            if (user == null || Array.IndexOf(ValidPrivileges, strNewPrivilege) < 0)
            {
                return false;
            }

            user.priviledge = strNewPrivilege;
            return true;
        }

        /// <summary>
        /// Updates the master key used to encrypt the passwords
        /// </summary>
        /// <returns>Returns a List of new username-password pairs. A highly good idea to dispose of this object ASAP.</returns>
        public List<string[]> UpdateKey()
        {
            // first, generate a new key
            Aes newKey = Aes.Create();
            
            // next, update the key access everyone has
            // NOTE: We reset everyone's password.
            // The key would be updated to prevent a person who previously had access from having access again
            SHA256 shaHash = SHA256.Create();
            byte[] data;
            byte[] salt = new byte[SaltLength];
            int passwordLength = 12;
            byte[] newPassword = new byte[passwordLength];
            byte[] saltPlusPassword = new byte[SaltLength + passwordLength];

            List<string[]> usersAndPasswords = new List<string[]>(strUsers.Count);
            foreach (User user in strUsers)
            {
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    // generate salt
                    rngCsp.GetBytes(salt);
                    rngCsp.GetBytes(newPassword);
                }
                newPassword = sanitizeRandomBytes(newPassword);
                string sanitizedPassword = ConvertToString(newPassword);

                // combine the salt and password
                Array.Copy(salt, saltPlusPassword, SaltLength);
                Array.Copy(newPassword, 0, saltPlusPassword, SaltLength, passwordLength);

                data = shaHash.ComputeHash(shaHash.ComputeHash(saltPlusPassword));

                // build hash
                user.saltAndPassword = ConvertToString(salt) + "$" + ConvertToString(data);
                user.keyXORPassword = Convert.ToBase64String(ClsStorage.XOR(shaHash.ComputeHash(saltPlusPassword), newKey.Key));

                string[] userAndPassword = { user.name, sanitizedPassword };
                usersAndPasswords.Add(userAndPassword);
            }

            //finally, update key
            aesInfo.Key = newKey.Key;
            return usersAndPasswords;
        }

        private static byte[] sanitizeRandomBytes(byte[] byteArray)
        {
            for (int i = 0; i < byteArray.Length; i++)
            {
                int pos = byteArray[i] % allowedCharacters.Length;
                byteArray[i] = (byte)allowedCharacters[pos];
            }

            return byteArray;
        }

        /// <summary>
        /// Converts a byte array into a hexadecimal string. Used to be called bytesToHex.
        /// </summary>
        /// <param name="byteArray">Array of bytes</param>
        /// <returns>String in hexadecimal</returns>
        public static string ConvertToString(byte[] byteArray)
        {
            StringBuilder builder = new StringBuilder();
            for (int j = 0; j < byteArray.Length; j++)
                builder.Append(byteArray[j].ToString("x2"));
            return builder.ToString();
        }

        /// <summary>
        /// Reverses bytesToHex
        /// </summary>
        /// <param name="hex">The array to convert</param>
        /// <returns>An array of bytes equivalent to the input</returns>
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// Adds a member to the club member list
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="type">Type of member</param>
        /// <param name="uiID">ID number to give to this member</param>
        /// <param name="iFaculty">Faculty of member</param>
        /// <param name="strInstrument">Instrument which this member plays</param>
        /// <param name="strOtherInstrument">Other Instrument</param>
        /// <param name="strEmail">Email of user</param>
        /// <param name="strOther">Other info</param>
        /// <param name="shirtSize">Shirt size</param>
        /// <returns>Whether the member was successfully added</returns>
        public bool AddMember(string strFName, string strLName, Member.MemberType type, uint uiID, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail, string strOther, int shirtSize)
        {
            // before adding, check if it is a duplicate member
            // a matching student number or email address will be the judge of this
            // if it is a duplicate, then update the member's profile
            for (int i = 0; i < iMember; i++)
                if (this.members[i].email == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].EditMember(strFName, strLName, type, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, shirtSize);
                    return false;
                }

            this.members[iMember] = new Member(strFName, strLName, type, uiID, iFaculty, strInstrument, strOtherInstrument, strEmail, strOther, shirtSize);
            iMember++;
            return true;
        }

        /// <summary>
        /// Adds a member to the club member list
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="type">Type of member</param>
        /// <param name="uiID">ID number to give to this member</param>
        /// <param name="iFaculty">Faculty of member</param>
        /// <param name="strInstrument">Instrument which this member plays</param>
        /// <param name="strOtherInstrument">Other Instrument</param>
        /// <param name="strEmail">Email of user</param>
        /// <param name="strOther">Other info</param>
        /// <param name="shirtSize">Shirt size</param>
        /// <param name="bInstruments">Boolean array of whether a member plays each instrument</param>
        /// <returns>Whether the member was successfully added</returns>
        public bool AddMember(string strFName, string strLName, Member.MemberType type, uint uiID, int iFaculty, string strInstrument, string strOtherInstrument, string strEmail, string strOther, int shirtSize, bool[] bInstruments)
        {
            // before adding, check if it is a duplicate member
            // a matching student number or email address will be the judge of this
            // if it is a duplicate, then update the member's profile
            for (int i = 0; i < iMember; i++)
                if (this.members[i].email == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].EditMember(strFName, strLName, type, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, shirtSize);
                    return false;
                }

            this.members[iMember] = new Member(strFName, strLName, type, uiID, iFaculty, strInstrument, strOtherInstrument, strEmail, strOther, shirtSize, bInstruments);
            iMember++;
            return true;
        }

        /// <summary>
        /// Adds a member to the club member list
        /// this version is kept for legacy purposes to open old data.
        /// Potentially updates an old member instead of adding a new member
        /// </summary>
        /// <param name="strFName">First name</param>
        /// <param name="strLName">Last name</param>
        /// <param name="type">Type of member</param>
        /// <param name="uiID">ID number to give to this member</param>
        /// <param name="iFaculty">Faculty of member</param>
        /// <param name="strInstrument">Instrument which this member plays</param>
        /// <param name="strEmail">Email of user</param>
        /// <param name="strOther">Other info</param>
        /// <param name="signup">Time that this member signed up</param>
        /// <returns>Whether a strictly new member is added; if an old member is updated, this return false</returns>
        public bool AddMember(string strFName, string strLName, Member.MemberType type, uint uiID, int iFaculty, int iShirt, string strInstrument, string strEmail, string strOther, DateTime signup)
        {
            // before adding, check if it is a duplicate member
            // a matching student number or email address will be the judge of this
            for (int i = 0; i < iMember; i++)
                if (this.members[i].email == strEmail || (this.members[i].uiStudentNumber == uiID && uiID != 0))
                {
                    this.members[i].EditMember(strFName, strLName, type, uiID, iFaculty, strInstrument, strEmail, strOther, members[i].signupTime, -1);
                    return false;
                }

            this.members[iMember] = new Member(strFName, strLName, type, uiID, iFaculty, strInstrument, strEmail, strOther, signup, iShirt);
            iMember++;
            return true;
        }

        /// <summary>
        /// Removes members who have not attended a single rehearsal in 4 years from the club mailing list
        /// </summary>
        public void PurgeOldMembers()
        {
            bool attendedOneRehearsal;

            // for each member, check if they have attended any rehearsals
            // we aren't getting rid of members who have in fact attended a rehearsal and were once a member
            // this is to prevent the mailing list from getting too cluttered
            for (int i = 0; i < iMember; i++)
            {
                attendedOneRehearsal = false;

                // check each term to confirm they are not in any of them
                for (int j = 0; j < listTerms.Count && !attendedOneRehearsal; j++)
                {
                    attendedOneRehearsal = attendedOneRehearsal || listTerms[j].memberSearch((short)i) != -1;
                }

                // if they haven't attended any rehearsals, next check if they have been on the list for four years (1461 days)
                // just keep using the same attendedOneRehearsal variable
                attendedOneRehearsal = attendedOneRehearsal || TimeSpan.Compare(DateTime.Now - members[i].signupTime, TimeSpan.FromDays(1461)) < 0;

                // if they didn't meet at least one of these requirements... they gotta go!
                if (!attendedOneRehearsal)
                {
                    // bye bye!

                    // note: this algorithm isn't super efficient, but since it is going to be performed rarely, I saw no need in making it efficient
                    PurgeMember(i);

                    // reduce i, because we might have more members to remove!
                    i--;
                }
            }
        }

        public bool AddTerm(string strName, short index, short numRehearsals, DateTime start, DateTime end, DateTime[] rehearsalDates, double membershipFees, double[] dOtherFees = null, string[] strOtherFees = null)
        {
            if (listTerms == null) // no term has been added yet
                listTerms = new List<Term>(1);

            listTerms.Add(new Term(strName, index, numRehearsals, start, end, rehearsalDates, membershipFees, dOtherFees, strOtherFees));
            return true;

            // like adding member, always returns true
            // functionality to add false in case adding a term becomes more difficult
        }

        public string[] GetTermNames()
        {
            string[] output = new string[listTerms.Count];
            int i = 0;
            foreach (Term currentTerm in listTerms)
            {
                output[i] = currentTerm.strName;
                i++;
            }

            return output;
        }

        public string GetFormattedName(int index)
        {
            if (this.members[index].curInstrument != Member.Instrument.Other)
                return String.Format("{0}, {1}", GetFirstAndLastName(index), Member.instrumentToString(this.members[index].curInstrument));
            else
            {
                if (this.members[index].otherInstrument == null || this.members[index].otherInstrument == String.Empty)
                {
                    return String.Format("{0}", GetFirstAndLastName(index));
                }
                else
                {
                    return String.Format("{0}, {1}", GetFirstAndLastName(index), this.members[index].otherInstrument);
                }
            }
        }

        public string GetFirstAndLastName(int index)
        {
            return String.Format("{0} {1}", this.members[index].firstName, this.members[index].lastName);
        }

        /// <summary>
        /// Search for member's index by email. Returns -1 if not found.
        /// </summary>
        /// <param name="strEmail">Email Address of Member</param>
        /// <returns>An integer for the member with the given email, -1 if no member found</returns>
        public int FindMember(string strEmail)
        {
            for (int i = 0; i < iMember; i++)
                if (members[i].email == strEmail)
                    return i;
            return -1;
        }

        /// <summary>
        /// Search for member's index by its internal ID. Returns -1 if not found.
        /// </summary>
        /// <param name="sID">Internal ID.</param>
        /// <returns>An integer for the member with the given ID, -1 if no member found</returns>
        public int FindMember(short sID)
        {
            for (int i = 0; i < iMember; i++)
                if (members[i].sID == sID)
                    return i;
            return -1;
        }

        /// <summary>Adds an item to the club's budget </summary>
        /// <param name="val">Value of item</param>
        /// <param name="strName">Description for this budget item</param>
        /// <param name="dtDateOccur">Date of event</param>
        /// <param name="dtDateAccount">Date as per account</param>
        /// <param name="strCategory">Category of item</param>
        /// <param name="type">Whether this item is a revenue/expense/etc.</param>
        /// <param name="termIndex">Index of relevant term</param>
        /// <param name="strComment">Any comments from the user</param>
        /// <param name="asset">Index of the asset</param>
        public void AddBudget(double val, string strName, DateTime dtDateOccur, DateTime dtDateAccount, string strCategory, TransactionType type, int termIndex, string strComment, BudgetItem asset = null)
        {
            BudgetItem newItem = new BudgetItem();
            newItem.value = val;
            newItem.name = strName;
            newItem.dateOccur = dtDateOccur;
            newItem.dateAccount = dtDateAccount;
            newItem.cat = strCategory;
            newItem.type = type;
            newItem.term = termIndex;
            newItem.comment = strComment;

            // if depreciation
            if (type == TransactionType.Depreciation)
                newItem.depOfAsset = asset;

            this.budget.Add(newItem);
        }

        public void DeleteBudget(int index)
        {
            this.budget.RemoveAt(index);
        }

        /// <summary>
        /// Returns a list of the indexes of all assets
        /// </summary>
        /// <param name="withDepAssets">Whether to include assets that are depreciated</param>
        /// <returns>An array of budgetItem's</returns>
        public BudgetItem[] GetAssetList(bool withDepAssets)
        {
            List<BudgetItem> output = new List<BudgetItem>();
            foreach (BudgetItem item in this.budget)
            {
                // if asset and not fully depreciated
                if (item.type == TransactionType.Asset && (withDepAssets || !DetermineIsFullyDepreciated(item)))
                    output.Add(item);
            }

            return output.ToArray();
        }

        /// <summary>
        /// Determines if asset has any value after depreciation
        /// </summary>
        /// <param name="asset">the asset to check</param>
        /// <param name="beforeDate">Only include depreciation before this date</param>
        /// <returns>Returns true is the depreciation on the asset is at least the value of the asset</returns>
        public bool DetermineIsFullyDepreciated(BudgetItem asset, DateTime? beforeDate = null)
        {
            if (beforeDate == null)
            {
                beforeDate = DateTime.MaxValue;
            }

            // don't even to guess if the asset being depreciated hasn't been marked
            if (!ClsStorage.currentClub.budget.Contains(asset))
            {
                return false;
            }

            // sum up all of the depreciation against this asset
            double amountDepreciated = 0;
            foreach (BudgetItem currentItem in this.budget)
            {
                if (currentItem.type == TransactionType.Depreciation && currentItem.depOfAsset == asset && currentItem.dateOccur <= beforeDate)
                {
                    amountDepreciated += currentItem.value;
                }
            }

            return amountDepreciated >= asset.value;
        }

        public double CalculateValueAfterDepreciation(BudgetItem asset)
        {
            double dDep = 0;

            // sum up all of the depreciation against this asset
            foreach (BudgetItem itemIterator in this.budget)
                if (itemIterator.type == TransactionType.Depreciation && itemIterator.depOfAsset == asset)
                    dDep += itemIterator.value;
            return asset.value - dDep;
        }

        public void AddHistory(string additionalInfo, ChangeType type)
        {
            HistoryItem newItem = new HistoryItem(strCurrentUser, type, additionalInfo, DateTime.Now);
            historyList.Add(newItem);
            
            // mark unsaved changes
            ClsStorage.unsavedChanges = true;

            // if autosave is turned on, then save at this point
            if (Properties.Settings.Default.autoSave)
                Program.home.btnSave_Click(new object(), new EventArgs());
        }

        private void PurgeMember(int index)
        {
            // every member with a greater index needs to be adjusted
            // notably, the references in terms
            int termIndex;

            // first, fix the terms
            // then, move members into their new positions
            for (int i = index + 1; i < iMember; i++)
            {
                for (int j = 0; j < listTerms.Count; j++)
                {
                    // check if the member is in a term
                    termIndex = listTerms[j].memberSearch((short)i);

                    // if so, correct that members position in the term
                    if (termIndex != -1)
                        listTerms[j].members[termIndex]--;
                }

                // adjust the member's sID
                members[i].sID--;

                // adjust the member down
                members[i - 1] = members[i];
            }

            members[iMember] = null;
            iMember--;
        }
    }
}

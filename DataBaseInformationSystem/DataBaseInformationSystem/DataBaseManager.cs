using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DataBaseInformationSystem {
    internal class DataBaseManager {
        public static readonly DataBaseManager Instance = new DataBaseManager();

        MySqlConnection connection;

        const string SOFTWARE_TABLE = "software";
        const string DEVELOPERS_TABLE = "developers";
        const string CATEGORIES_TABLE = "categories";
        const string VERSIONS_TABLE = "versions";
        const string EMPLOYEES_TABLE = "employees";
        const string INSTALLATIONS_TABLE = "installations";
        const string INSTALLATION_ROOMS_TABLE = "installation_rooms";

        private DataBaseManager() { }

        public bool Connect() {
            connection = new MySqlConnection(getConnectionString());
            try {
                connection.Open();
                return true;
            } catch {
                return false;
            }
        }

        void checkConnection() {
            if (!connection.Ping()) {
                connection.Open();
            }
        }

        string getConnectionString() {
            return "Server=localhost;Port=3306;Database=installsdb;Uid=root;Pwd=11111;";
        }

        int count(string table, string column, string value) {
            checkConnection();

            int result = -1;

            MySqlCommand command = new MySqlCommand(
                $"SELECT COUNT(*) as count FROM {table} WHERE {column} = @value",
                connection);

            command.Parameters.Add(
                new MySqlParameter($"@value", MySqlDbType.String) {
                    Value = value
                });

            using (MySqlDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    result = reader.GetInt32("count");
                }
            }

            command.Dispose();

            return result;
        }

        int select(string table, string columnToSelect, string column, string value) {
            checkConnection();

            int result = -1;

            MySqlCommand command = new MySqlCommand(
                $"SELECT {columnToSelect} FROM {table} WHERE {column} = @value",
                connection);

            command.Parameters.Add(
                new MySqlParameter($"@value", MySqlDbType.String) {
                    Value = value
                });

            using (MySqlDataReader reader = command.ExecuteReader()) {
                while (reader.Read()) {
                    result = reader.GetInt32(columnToSelect);
                }
            }

            command.Dispose();

            return result;
        }

        int insert(string table, string column, string value) {
            checkConnection();

            MySqlCommand insertCommand = new MySqlCommand(
                $"INSERT INTO {table} ({column}) VALUES (@column)",
                connection);

            insertCommand.Parameters.Add(
                new MySqlParameter($"@column", MySqlDbType.String) {
                    Value = value
                });

            insertCommand.ExecuteNonQuery();

            long insertedId = insertCommand.LastInsertedId;

            insertCommand.Dispose();

            return (int)insertedId;
        }

        bool update(string table, string column, string newValue, int id) {
            checkConnection();

            MySqlCommand updateCommand = new MySqlCommand(
                $"UPDATE {table} SET {column} = @column WHERE id = @id",
                connection);

            updateCommand.Parameters.Add(new MySqlParameter($"@column", MySqlDbType.String) {
                Value = newValue
            });

            updateCommand.Parameters.Add(new MySqlParameter($"@id", MySqlDbType.Int32) {
                Value = id
            });

            int affectedRows = updateCommand.ExecuteNonQuery();
            updateCommand.Dispose();

            return affectedRows > 0;
        }

        bool delete(string table, int id) {
            checkConnection();

            MySqlCommand deleteCommand = new MySqlCommand(
                $"DELETE FROM {table} WHERE id = @id",
                connection);

            deleteCommand.Parameters.Add(new MySqlParameter($"@id", MySqlDbType.Int32) {
                Value = id
            });

            int affectedRows = deleteCommand.ExecuteNonQuery();
            deleteCommand.Dispose();

            return affectedRows > 0;
        }

        int[] getIds(string table, string column, int id) {
            List<int> idsToDelete = new List<int>();
            MySqlCommand selectCommand = new MySqlCommand($"SELECT id FROM {table} WHERE {column} = {id}", connection);
            using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                while (reader.Read()) {
                    idsToDelete.Add(reader.GetInt32("id"));
                }
                reader.Close();
            }
            selectCommand.Dispose();
            return idsToDelete.ToArray();
        }

        #region Installs

        public List<InstallInfo> SelectInstalls() {
            checkConnection();

            List<InstallInfo> result = new List<InstallInfo>();

            MySqlCommand selectCommand = new MySqlCommand(
                $"SELECT i.id, s.name as software_name, d.name as developer_name, c.name as category, e.full_name as employee_full_name, v.name as version, i.installation_date" +
                $" FROM {INSTALLATIONS_TABLE} i" +
                $" JOIN {SOFTWARE_TABLE} s ON i.software_id = s.id" +
                $" JOIN {DEVELOPERS_TABLE} d ON s.developer_id = d.id" +
                $" JOIN {CATEGORIES_TABLE} c ON s.category_id = c.id" +
                $" JOIN {EMPLOYEES_TABLE} e ON i.employee_id = e.id" +
                $" JOIN {VERSIONS_TABLE} v ON i.version_id = v.id",
        connection);

            using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                while (reader.Read()) {
                    result.Add(new InstallInfo {
                        Id = reader.GetInt32("id"),
                        SoftwareName = reader.GetString("software_name"),
                        SoftwareDeveloper = reader.GetString("developer_name"),
                        SoftwareCategory = reader.GetString("category"),
                        EmployeeFullName = reader.GetString("employee_full_name"),
                        Version = reader.GetString("version"),
                        Date = reader.GetDateTime("installation_date").ToString("dd.MM.yyyy")
                    });
                }
            }

            selectCommand.Dispose();

            foreach (InstallInfo info in result) {
                info.Rooms = string.Empty;

                selectCommand = new MySqlCommand(
                    $"SELECT room_number FROM {INSTALLATION_ROOMS_TABLE} r WHERE r.installation_id = @id",
                    connection);

                selectCommand.Parameters.Add(new MySqlParameter($"@id", MySqlDbType.Int32) {
                    Value = info.Id
                });

                using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                    if (reader.Read()) {
                        while (true) {
                            info.Rooms += reader.GetInt32("room_number").ToString();

                            if (reader.Read()) {
                                info.Rooms += ", ";
                            } else {
                                break;
                            }
                        }
                    }
                }

                selectCommand.Dispose();
            }

            return result;
        }

        public int InsertInstall(int softwareId, int employeeId, string version, DateTime installDate) {
            checkConnection();

            MySqlCommand insertCommand = new MySqlCommand(
                $"INSERT INTO {INSTALLATIONS_TABLE}" +
                $" (software_id, employee_id, version_id, installation_date) VALUES (@softwareId, @employeeId, @versionId, @date)",
                connection);

            insertCommand.Parameters.Add(
                new MySqlParameter($"@softwareId", MySqlDbType.Int32) {
                    Value = softwareId
                });
            insertCommand.Parameters.Add(
                new MySqlParameter($"@employeeId", MySqlDbType.Int32) {
                    Value = employeeId
                });
            insertCommand.Parameters.Add(
                new MySqlParameter($"@versionId", MySqlDbType.Int32) {
                    Value = InsertVersion(version)
                });
            insertCommand.Parameters.Add(
                new MySqlParameter($"@date", MySqlDbType.Date) {
                    Value = installDate
                });

            insertCommand.ExecuteNonQuery();

            long insertedId = insertCommand.LastInsertedId;

            insertCommand.Dispose();

            return (int)insertedId;
        }

        public int InsertInstallRoom(int installId, int roomNumber) {
            checkConnection();

            MySqlCommand insertCommand = new MySqlCommand(
                $"INSERT INTO {INSTALLATION_ROOMS_TABLE}" +
                $" (installation_id, room_number) VALUES (@installationId, @roomNumber)",
                connection);

            insertCommand.Parameters.Add(
                new MySqlParameter($"@installationId", MySqlDbType.Int32) {
                    Value = installId
                });
            insertCommand.Parameters.Add(
                new MySqlParameter($"@roomNumber", MySqlDbType.Int32) {
                    Value = roomNumber
                });

            try {
                insertCommand.ExecuteNonQuery();
            } catch {
                insertCommand.Dispose();
                return -1;
            }

            long insertedId = insertCommand.LastInsertedId;

            insertCommand.Dispose();

            return (int)insertedId;
        }

        public int InsertVersion(string name) {
            if (count(VERSIONS_TABLE, "name", name) == 0) {
                return insert(VERSIONS_TABLE, "name", name);
            }
            return select(VERSIONS_TABLE, "id", "name", name);
        }

        public bool UpdateInstall(int id, int newSoftwareId, int newEmployeeId, string newVersion, DateTime newInstallDate) {
            checkConnection();

            MySqlCommand updateCommand = new MySqlCommand(
                $"UPDATE {INSTALLATIONS_TABLE}" +
                $" SET software_id = @softwareId," +
                $" employee_id = @employeeId," +
                $" version_id = @versionId," +
                $" installation_date = @installationDate" +
                $" WHERE id = @id",
                connection);

            updateCommand.Parameters.Add(new MySqlParameter($"@softwareId", MySqlDbType.Int32) {
                Value = newSoftwareId
            });

            updateCommand.Parameters.Add(new MySqlParameter($"@employeeId", MySqlDbType.Int32) {
                Value = newEmployeeId
            });

            updateCommand.Parameters.Add(new MySqlParameter($"@versionId", MySqlDbType.Int32) {
                Value = InsertVersion(newVersion)
            });

            updateCommand.Parameters.Add(new MySqlParameter($"@installationDate", MySqlDbType.Date) {
                Value = newInstallDate
            });

            updateCommand.Parameters.Add(new MySqlParameter($"@id", MySqlDbType.Int32) {
                Value = id
            });

            int affectedRows = updateCommand.ExecuteNonQuery();
            updateCommand.Dispose();

            return affectedRows > 0;
        }

        public bool DeleteInstallRooms(int installId) {
            checkConnection();

            MySqlCommand deleteCommand = new MySqlCommand(
                $"DELETE FROM {INSTALLATION_ROOMS_TABLE}" +
                $"  WHERE installation_id = @installationId",
                connection);

            deleteCommand.Parameters.Add(
                new MySqlParameter($"@installationId", MySqlDbType.Int32) {
                    Value = installId
                });

            int affectedRows = deleteCommand.ExecuteNonQuery();
            deleteCommand.Dispose();

            return affectedRows > 0;
        }

        public bool DeleteInstall(int id) {
            DeleteInstallRooms(id);
            return delete(INSTALLATIONS_TABLE, id);
        }

        #endregion

        #region Software

        public List<SoftwareInfo> SelectSoftware() {
            checkConnection();

            List<SoftwareInfo> result = new List<SoftwareInfo>();

            MySqlCommand selectCommand = new MySqlCommand(
                $"SELECT s.id AS id, s.name AS software_name, d.name AS developer_name, c.name AS category_name"
                + " FROM software s "
                + $"JOIN {DEVELOPERS_TABLE} d ON s.developer_id = d.id JOIN {CATEGORIES_TABLE} c on s.category_id = c.id;",
                connection);

            using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                while (reader.Read()) {
                    result.Add(new SoftwareInfo {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("software_name"),
                        Category = reader.GetString("category_name"),
                        Developer = reader.GetString("developer_name")
                    });
                }
            }

            return result;
        }

        public int InsertSoftware(string name, int developerId, int categoryId) {
            checkConnection();

            MySqlCommand insertCommand = new MySqlCommand(
                $"INSERT INTO {SOFTWARE_TABLE}" +
                $" (name, developer_id, category_id) VALUES (@name, @developerId, @categoryId)",
                connection);

            insertCommand.Parameters.Add(
                new MySqlParameter($"@name", MySqlDbType.String) {
                    Value = name
                });
            insertCommand.Parameters.Add(
                new MySqlParameter($"@developerId", MySqlDbType.Int32) {
                    Value = developerId
                });
            insertCommand.Parameters.Add(
                new MySqlParameter($"@categoryId", MySqlDbType.Int32) {
                    Value = categoryId
                });

            insertCommand.ExecuteNonQuery();

            long insertedId = insertCommand.LastInsertedId;

            insertCommand.Dispose();

            return (int)insertedId;
        }

        public bool UpdateSoftware(int id, string newName, int newDeveloperId, int newCategoryId) {
            checkConnection();

            MySqlCommand updateCommand = new MySqlCommand(
                $"UPDATE {SOFTWARE_TABLE} SET name = @name, developer_id = @dId, category_id = @cId WHERE id = @id",
                connection);

            updateCommand.Parameters.Add(new MySqlParameter($"@name", MySqlDbType.String) {
                Value = newName
            });

            updateCommand.Parameters.Add(new MySqlParameter($"@id", MySqlDbType.Int32) {
                Value = id
            });

            updateCommand.Parameters.Add(new MySqlParameter($"@dId", MySqlDbType.Int32) {
                Value = newDeveloperId
            });

            updateCommand.Parameters.Add(new MySqlParameter($"@cId", MySqlDbType.Int32) {
                Value = newCategoryId
            });

            int affectedRows = updateCommand.ExecuteNonQuery();
            updateCommand.Dispose();

            return affectedRows > 0;
        }

        public bool DeleteSoftware(int id) {
            checkConnection();

            foreach (int item in getIds(INSTALLATIONS_TABLE, "software_id", id)) DeleteInstall(item);
            
            return delete(SOFTWARE_TABLE, id);
        }

        #endregion

        #region Developers

        public List<DeveloperInfo> SelectDevelopers() {
            checkConnection();

            List<DeveloperInfo> result = new List<DeveloperInfo>();

            MySqlCommand selectCommand = new MySqlCommand(
                $"SELECT id, name FROM {DEVELOPERS_TABLE}",
                connection);

            using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                while (reader.Read()) {
                    result.Add(new DeveloperInfo {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    });
                }
            }

            return result;
        }

        public bool UpdateDeveloper(int id, string newName) {
            return update(DEVELOPERS_TABLE, "name", newName, id);
        }

        public int InsertDeveloper(string name) {
            return insert(DEVELOPERS_TABLE, "name", name);
        }

        public bool DeleteDeveloper(int id) {
            checkConnection();

            foreach (int item in getIds(SOFTWARE_TABLE, "developer_id", id)) DeleteSoftware(item);

            return delete(DEVELOPERS_TABLE, id);
        }

        #endregion

        #region Categories

        public List<CategoryInfo> SelectCategories() {
            checkConnection();

            List<CategoryInfo> result = new List<CategoryInfo>();

            MySqlCommand selectCommand = new MySqlCommand(
                $"SELECT id, name FROM {CATEGORIES_TABLE}",
                connection);

            using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                while (reader.Read()) {
                    result.Add(new CategoryInfo {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name")
                    });
                }
            }

            return result;
        }

        public int InsertCategory(string name) {
            return insert(CATEGORIES_TABLE, "name", name);
        }

        public bool UpdateCategory(int id, string newName) {
            return update(CATEGORIES_TABLE, "name", newName, id);
        }

        public bool DeleteCategory(int id) {
            checkConnection();

            foreach (int item in getIds(SOFTWARE_TABLE, "category_id", id)) DeleteSoftware(item);

            return delete(CATEGORIES_TABLE, id);
        }

        #endregion

        #region Employees

        public List<EmployeeInfo> SelectEmployees() {
            checkConnection();

            List<EmployeeInfo> result = new List<EmployeeInfo>();

            MySqlCommand selectCommand = new MySqlCommand(
                $"SELECT id, full_name FROM {EMPLOYEES_TABLE}",
                connection);

            using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                while (reader.Read()) {
                    result.Add(new EmployeeInfo {
                        Id = reader.GetInt32("id"),
                        FullName = reader.GetString("full_name")
                    });
                }
            }

            return result;
        }

        public int InsertEmployee(string fullName) {
            return insert(EMPLOYEES_TABLE, "full_name", fullName);
        }

        public bool UpdateEmployee(int id, string newName) {
            return update(EMPLOYEES_TABLE, "full_name", newName, id);
        }

        public bool DeleteEmployee(int id) {
            checkConnection();
            
            foreach (int item in getIds(INSTALLATIONS_TABLE, "employee_id", id)) DeleteInstall(item);

            return delete(EMPLOYEES_TABLE, id);
        }

        #endregion

        #region Rooms

        public int[] SelectRooms() {
            checkConnection();

            List<int> result = new List<int>();

            MySqlCommand selectCommand = new MySqlCommand(
                $"SELECT DISTINCT room_number FROM {INSTALLATION_ROOMS_TABLE} ORDER BY room_number ASC",
                connection);

            using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                while (reader.Read()) {
                    result.Add(reader.GetInt32("room_number"));
                }
            }

            selectCommand.Dispose();

            return result.ToArray();
        }

        #endregion

        #region Dates

        public DateTime? SelectMinDateTime() {
            checkConnection();

            DateTime? result = null;

            MySqlCommand selectCommand = new MySqlCommand(
                $"SELECT installation_date FROM {INSTALLATIONS_TABLE} ORDER BY installation_date ASC LIMIT 1",
                connection);

            using (MySqlDataReader reader = selectCommand.ExecuteReader()) {
                while (reader.Read()) {
                    result = reader.GetDateTime("installation_date");
                }
            }

            selectCommand.Dispose();

            return result;
        }

        #endregion
    }
}
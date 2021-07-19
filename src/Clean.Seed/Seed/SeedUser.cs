using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Clean.DataAccess.Entities.Users;
using Clean.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static Clean.Core.Enums;

namespace Clean.Seed
{
    internal static partial class Seeding
    {
        internal static void SeedUser(CleanDbContext context)
        {
            int active = (int)ActiveStatus.Active;
            int inactive = (int)ActiveStatus.Inactive;
            int deleted = (int)ActiveStatus.Deleted;

            DateTime utcNow = DateTime.UtcNow;

            string filePath = Utils.Data.GetSeedDataFromFile("user.json");

            Log.Information($"File path: {filePath}");

            string jsonData = File.ReadAllText(filePath);

            List<User> users = JsonSerializer.Deserialize<List<User>>(jsonData);

            Log.Information($"Total records to be inserted/updated: {users.Count}");
            Log.Information($"Before seeding: Total records in database (active/inactive): {context.Users.Count(a => a.ActiveStatus == active || a.ActiveStatus == inactive)}");
            Log.Information($"Before seeding: Total records in database (deleted): {context.Users.Count(a => a.ActiveStatus == deleted)}");

            int insertedCount = 0;
            int updatedCount = 0;

            foreach (var user in users)
            {
                Log.Information($"---");

                user.ActiveStatus = active;

                var dbRecord = context.Users.Find(user.UserId);

                var jsonSerializerOption = new JsonSerializerOptions { ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles };

                if (dbRecord != null)
                {
                    Log.Information($"Existing record found in database:");
                    Log.Information($"{JsonSerializer.Serialize(dbRecord, jsonSerializerOption)}");

                    updatedCount++;
                    dbRecord.ExternalUserId = user.ExternalUserId;
                    dbRecord.UserName = user.UserName;
                    dbRecord.Email = user.Email;
                    dbRecord.DisplayName = user.DisplayName;
                    dbRecord.UpdatedById = user.UpdatedById;

                    Log.Information($"Record to be updated:");
                    Log.Information($"{JsonSerializer.Serialize(dbRecord, jsonSerializerOption)}");

                    context.Users.Update(dbRecord);

                    Log.Information($"Updating record in database");
                }
                else
                {
                    Log.Information($"Record not found in database");

                    insertedCount++;

                    Log.Information($"Record to be inserted:");
                    Log.Information($"{JsonSerializer.Serialize(user, jsonSerializerOption)}");

                    context.Users.Add(user);

                    Log.Information($"Inserting record to database");
                }

                context.Database.OpenConnection();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.[User] ON");

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Error($"Error when inserting record to database: {ex.Message}");
                    Log.Error($"Error when inserting record to database: {ex.InnerException}");
                    Log.Error($"Error when inserting record to database: {ex.StackTrace}");
                }

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.[User] OFF");
                context.Database.CloseConnection();

                Log.Information($"---");
            }

            Log.Information($"Total records updated: {updatedCount}");
            Log.Information($"Total records inserted: {insertedCount}");
            Log.Information($"After seeding: Total records in database (deleted): {context.Users.Count(a => a.ActiveStatus == deleted)}");
            Log.Information($"After seeding: Total records in database (active/inactive): {context.Users.Count(a => a.ActiveStatus == active || a.ActiveStatus == inactive)}");
        }
    }
}

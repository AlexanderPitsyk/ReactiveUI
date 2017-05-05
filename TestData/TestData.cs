using System;
using System.Linq;
using ReactiveUIApplication.Models;
using ReactiveUIApplication.Repositories;

namespace ReactiveUIApplication.TestData
{
    public static class TestData
    {
        public static void SetTestData()
        {
            using (var db = new UserContext())
            {
                if (db.Users.Any())
                {
                    return;
                }

                db.Users.Add(new User("x"));
                db.SaveChanges();

                db.Credentials.Add(new Credential(db.Users.First(e => e.Name == "x"), "x"));
                db.SaveChanges();
            }

            using (var db = new ToDoContext())
            {
                if (db.ToDoItemList.Any())
                {
                    return;
                }

                db.ToDoItemList.Add(new ToDoItem
                {
                    PriorityId = 1,
                    Name = "Wash dishes",
                    Created = DateTime.Now,
                    Description = "Wash all dishes, spoon, fork",
                    DueDate = DateTime.Now,
                    IsDone = false
                });
                db.ToDoItemList.Add(new ToDoItem
                {
                    PriorityId = 2,
                    Name = "Cooking",
                    Created = DateTime.Now,
                    Description = "Cooking something",
                    DueDate = DateTime.Now,
                    IsDone = false
                });
                db.ToDoItemList.Add(new ToDoItem
                {
                    PriorityId = 3,
                    Name = "Cleanup house",
                    Created = DateTime.Now,
                    Description = "Cleanup house",
                    DueDate = DateTime.Now,
                    IsDone = false
                });
                db.SaveChanges();
            }
        }
    }
}
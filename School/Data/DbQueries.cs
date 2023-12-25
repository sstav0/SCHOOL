using School.Models;

namespace School.Data
{
    /// <summary>
    /// Provides static methods for performing database operations related to students, teachers, activities, and evaluations.
    /// </summary>
    /// <remarks>
    /// The <see cref="DbQueries"/> class encapsulates the logic for accessing and manipulating the data in the database.
    /// It includes methods for loading, altering, and deleting entities such as students, teachers, activities, and evaluations.
    /// Each method creates a new instance of <see cref="DatabaseContext"/> to interact with the database,
    /// ensuring that the database connection is properly managed and disposed of.
    /// </remarks>
    public class DbQueries
    {
        /// <summary>
        /// Retrieves all students from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="Student"/> objects representing all students in the database.
        /// </returns>
        /// <remarks>
        /// This method creates a new instance of <see cref="DatabaseContext"/> to access the database.
        /// It should be used with care in scenarios where performance is critical, as each call 
        /// opens and closes a new database connection.
        /// </remarks>
        public static List<Student> LoadAllStudents()
        {
            using (var db = new DatabaseContext())
            {
                return db.Students.ToList();
            }
        }

        /// <summary>
        /// Retrieves all teachers from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="Teacher"/> objects representing all teachers in the database.
        /// </returns>
        /// <remarks>
        /// This method creates a new instance of <see cref="DatabaseContext"/> to access the database.
        /// It should be used with care in scenarios where performance is critical, as each call 
        /// opens and closes a new database connection.
        /// </remarks>
        public static List<Teacher> LoadAllTeachers()
        {
            using (var db = new DatabaseContext())
            {
                return db.Teachers.ToList();
            }
        }

        /// <summary>
        /// Retrieves all activites from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="Activity"/> objects representing all activities in the database.
        /// </returns>
        /// <remarks>
        /// This method creates a new instance of <see cref="DatabaseContext"/> to access the database.
        /// It should be used with care in scenarios where performance is critical, as each call 
        /// opens and closes a new database connection.
        /// </remarks>
        public static List<Activity> LoadAllActivities()
        {
            using (var db = new DatabaseContext())
            {
                return db.Activities.ToList();
            }
        }

        /// <summary>
        /// Retrieves all evaluations from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="Evaluation"/> objects representing all evaluations in the database.
        /// </returns>
        /// <remarks>
        /// This method creates a new instance of <see cref="DatabaseContext"/> to access the database.
        /// It should be used with care in scenarios where performance is critical, as each call
        /// opens and closes a new database connection.
        /// </remarks>
        public static List<Evaluation> LoadAllEvaluations()
        {
            using (var db = new DatabaseContext())
            {
                return db.Evaluations.ToList();
            }
        }

        /// <summary>
        /// Retrieves a student by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the student to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Student"/> object corresponding to the specified ID, or null if no student is found.
        /// </returns>
        public static Student LoadStudentById(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Students.Find(id);
            }
        }

        /// <summary>
        /// Retrieves a teacher by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the teacher to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Teacher"/> object corresponding to the specified ID, or null if no teacher is found.
        /// </returns>
        public static Teacher LoadTeacherById(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Teachers.Find(id);
            }
        }

        /// <summary>
        /// Retrieves an activity by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the activity to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Activity"/> object corresponding to the specified ID, or null if no activity is found.
        /// </returns>
        public static Activity LoadActivityById(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Activities.Find(id);
            }
        }

        /// <summary>
        /// Retrieves an evaluation by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the evaluation to retrieve.
        /// </param>
        /// <returns>
        /// A <see cref="Activity"/> object corresponding to the specified ID, or null if no evaluation is found.
        /// </returns>
        public static Evaluation LoadEvaluationById(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return db.Evaluations.Find(id);
            }
        }

        /// <summary>
        /// Alters a student by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the student to alter.
        /// </param>
        /// <param name="values">
        /// A dictionary of values to alter, with the key being the name of the property to alter and the value being the new value. 
        /// Eg. {"firstname": "John", "lastname": "Doe"}
        /// </param>
        public static void AlterStudent(Guid id, Dictionary<string, string> values)
        {
            using (var db = new DatabaseContext())
            {
                var student = db.Students.Find(id);
                foreach (var value in values)
                {
                    switch (value.Key)
                    {
                        case "firstname":
                            student.Firstname = value.Value;
                            break;
                        case "lastname":
                            student.Lastname = value.Value;
                            break;
                    }
                }
                db.Students.Update(student);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Alters a teacher by their unique identifier from the database.
        /// <example> 
        /// For example : 
        /// <c>AlterTeacher(teacher.id, {"firstname": "John", "lastname": "Doe", "salary": "1000"}) </c>
        /// </example>
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the teacher to alter.
        /// </param>
        /// <param name="values">
        /// A dictionary of values to alter, with the key being the name of the property to alter and the value being the new value.
        /// </param>
        public static void AlterTeacher(Guid id, Dictionary<string, string> values)
        {
            using (var db = new DatabaseContext())
            {
                var teacher = db.Teachers.Find(id);
                int salary;
                foreach (var value in values)
                {
                    switch (value.Key)
                    {
                        case "firstname":
                            teacher.Firstname = value.Value;
                            break;
                        case "lastname":
                            teacher.Lastname = value.Value;
                            break;
                        case "salary":
                            if (!Int32.TryParse(value.Value, out salary)) // Handle the parse failure (e.g., set a default value or throw an exception)
                            {
                                salary = 0;
                            }
                            teacher.Salary = salary;
                            break;
                    }
                }
                db.Teachers.Update(teacher);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a teacher by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the teacher to delete.
        /// </param>
        public static void DeleteTeacher(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                var teacher = db.Teachers.Find(id);
                db.Teachers.Remove(teacher);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a student by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the student to delete.
        /// </param>
        public static void DeleteStudent(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                var student = db.Students.Find(id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an activity by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the activity to delete.
        /// </param>
        public static void DeleteActivity(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                var activity = db.Activities.Find(id);
                db.Activities.Remove(activity);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an evaluation by their unique identifier from the database.
        /// </summary>
        /// <param name="id">
        /// The unique identifier (ID) of the evaluation to delete.
        /// </param>
        public static void DeleteEvaluation(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                var evaluation = db.Evaluations.Find(id);
                db.Evaluations.Remove(evaluation);
                db.SaveChanges();
            }
        }



    }
}
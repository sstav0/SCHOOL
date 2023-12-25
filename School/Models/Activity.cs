namespace School.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;


/// <summary>
/// Represents an academic activity or course, including details such as the teacher and associated evaluations.
/// To add an activity to the database, you must first assign it to a teacher with the <c>AssignTeacher(teacher)</c> method.
/// Then it will be automatically added to the database.
/// </summary>
public class Activity
{
    /// <summary>
    /// Gets the unique identifier for the activity.
    /// </summary>
    public Guid ID { get; private set; }

    /// <summary>
    /// Gets the ECTS (European Credit Transfer and Accumulation System) credits for the activity.
    /// </summary>
    public int Ects { get; private set; }

    /// <summary>
    /// Gets the name of the activity.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets or sets the teacher associated with this activity.
    /// </summary>
    public Teacher Teacher { get; set; }

    /// <summary>
    /// Gets or sets the list of evaluations associated with this activity.
    /// This represents a one-to-many relationship with the Evaluation class.
    /// </summary>
    public List<Evaluation> Evaluations { get; set; }

    /// <summary>
    /// Initializes a new instance of the Activity class with a specified name and ECTS credits.
    /// </summary>
    /// <param name="name">The name of the activity.</param>
    /// <param name="ects">The ECTS credits for the activity.</param>
    public Activity(string name, int ects)
    {
        Evaluations = new List<Evaluation>();
        Name = name;
        Ects = ects;
    }

    /// <summary>
    /// Assigns the specified teacher to this activity and saves it to the database.
    /// </summary>
    /// <param name="teacher">The teacher to be assigned to this activity.</param>
    public void AssignTeacher(Teacher teacher)
    {
        Teacher = teacher;
        using (var db = new DatabaseContext())
        {
            if (db.Entry(Teacher).State == EntityState.Detached)
            {
                db.Teachers.Attach(teacher);
            }
            db.Activities.Add(this);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// Returns a string that represents the current activity.
    /// </summary>
    /// <returns>A string that represents the current activity.</returns>
    public override string ToString()
    {
        return String.Format("[{0}] {1} ({2})", ID, Name, Teacher);
    }
}

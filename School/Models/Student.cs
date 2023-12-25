namespace SCHOOL.Models;
using Microsoft.EntityFrameworkCore;
using SCHOOL.Data;

/// <summary>
/// Represents a student in the school.
/// To add a student to the database, use the <c>Add()</c> method.
/// </summary>
public class Student : Person
{
    /// <summary>
    /// Gets or sets the list of evaluations associated with the student.
    /// This represents a one-to-many relationship with the Evaluation class.
    /// </summary>
    public List<Evaluation> Evaluations { get; set; }

    /// <summary>
    /// Initializes a new instance of the Student class with specified first and last names.
    /// </summary>
    /// <param name="firstname">First name of the student.</param>
    /// <param name="lastname">Last name of the student.</param>
    public Student(string firstname, string lastname) : base(firstname, lastname)
    {
        Evaluations = new List<Evaluation>();
    }

    /// <summary>
    /// Adds this student instance to the database.
    /// </summary>
    public void Add()
    {
        using (var db = new DatabaseContext())
        {
            db.Students.Add(this);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// Returns a string that represents the current student.
    /// </summary>
    /// <returns>A string that represents the current student : <c>$"{Firstname} {Lastname}"</c></returns>
    public override string ToString()
    {
        return $"{Firstname} {Lastname}";
    }

    /// <summary>
    /// Loads all evaluations related to this student from the database.
    /// Eagerly includes related Activity data for each evaluation.
    /// </summary>
    public void LoadEvaluations()
    {
        using (var db = new DatabaseContext())
        {
            Evaluations = db.Evaluations
                            .Where(e => e.Student.ID == ID)
                            .Include(e => e.Activity)
                            .ToList();
        }
    }

    /// <summary>
    /// Calculates and returns the average score of the student based on evaluations.
    /// Takes into account the ECTS credits of each activity.
    /// </summary>
    /// <returns>The average score as a double.</returns>
    public double Average()
    {
        int total = 0;
        int ects = 0;
        foreach (var evaluation in Evaluations)
        {
            Activity activity = evaluation.Activity;
            total += evaluation.Note * activity.Ects;
            ects += activity.Ects;
        }
        return ects == 0 ? 0 : (double)total / ects;
    }

    /// <summary>
    /// Generates and returns a bulletin (report) for the student.
    /// Includes all evaluations and the calculated average score.
    /// </summary>
    /// <returns>A string representing the student's bulletin.</returns>
    public string Bulletin()
    {
        var lines = new List<String>
        {
            String.Format("Bulletin de {0}", ToString())
        };

        foreach (var evaluation in Evaluations)
        {
            lines.Add(evaluation.ToString());
        }

        lines.Add(String.Format("Moyenne: {0}", Average()));

        return String.Join("\n", lines);
    }
}

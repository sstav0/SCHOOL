namespace School.Models;
using System.ComponentModel.DataAnnotations.Schema;
using School.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Represents an evaluation, including details about the student, activity, and the score.
/// To add an evaluation to the database, you must first assign it to a student and an activity with the <c>AssignInfos(activity, student)</c> method.
/// Then it will be automatically added to the database.
/// You can either set the score with the <c>setScore(note)</c> method or with the <c>setAppreciation(appreciation)</c> method depending on the type of evaluation.
/// </summary>
public class Evaluation
{
    /// <summary>
    /// Gets the unique identifier for the evaluation.
    /// </summary>
    public Guid ID { get; private set; }

    /// <summary>
    /// Gets or sets the student associated with this evaluation.
    /// </summary>
    public Student Student { get; set; }

    /// <summary>
    /// Gets or sets the activity associated with this evaluation.
    /// </summary>
    public Activity Activity { get; set; }

    /// <summary>
    /// Gets the score of the evaluation.
    /// </summary>
    public int Note { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Evaluation class with a specified score.
    /// </summary>
    /// <param name="note">The score of the evaluation.</param>
    public Evaluation(int note)
    {
        ID = Guid.NewGuid();
        Note = note;
    }

    /// <summary>
    /// Assigns the specified activity and student to this evaluation and saves it to the database.
    /// </summary>
    /// <param name="activity">The activity to associate with this evaluation.</param>
    /// <param name="student">The student to associate with this evaluation.</param>
    public void AssignInfos(Activity activity, Student student)
    {
        Activity = activity;
        Student = student;
        using (var db = new DatabaseContext())
        {
            if (db.Entry(Activity).State == EntityState.Detached)
            {
                db.Activities.Attach(Activity);
            }

            if (db.Entry(Student).State == EntityState.Detached)
            {
                db.Students.Attach(Student);
            }

            db.Evaluations.Add(this);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// Sets the score of the evaluation.
    /// </summary>
    /// <param name="note">The new score to be set.</param>
    public void setScore(int note)
    {
        Note = note;
    }

    /// <summary>
    /// Sets the score based on a qualitative appreciation.
    /// </summary>
    /// <param name="appreciation">The qualitative appreciation (e.g., "X", "TB", "B", "C", "N").</param>
    public void setAppreciation(string appreciation)
    {
        switch (appreciation)
        {
            case "X":
                Note = 20;
                break;
            case "TB":
                Note = 16;
                break;
            case "B":
                Note = 12;
                break;
            case "C":
                Note = 8;
                break;
            case "N":
                Note = 4;
                break;
            default:
                Note = 0;
                break;
        }
    }

    /// <summary>
    /// Returns a string that represents the current evaluation.
    /// </summary>
    /// <returns>A string that represents the current evaluation.</returns>
    public override string ToString()
    {
        return String.Format("{0}: {1}/20", Activity, Note);
    }
}

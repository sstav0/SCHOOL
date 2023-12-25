namespace SCHOOL.Models;
using Microsoft.EntityFrameworkCore;
using SCHOOL.Data;
using System;

/// <summary>
/// Represents a teacher, inheriting from the Person class.
/// Includes teacher-specific properties like salary and methods for managing activities.
/// To add a teacher to the database, use the <c>Add()</c> method.
/// </summary>
public class Teacher : Person
{
    /// <summary>
    /// Gets or sets the salary of the teacher.
    /// </summary>
    public int Salary { get; set; }

    /// <summary>
    /// Gets or sets the list of activities associated with the teacher.
    /// This represents a one-to-many relationship with the Activity class.
    /// </summary>
    public List<Activity> Activities { get; set; }

    /// <summary>
    /// Initializes a new instance of the Teacher class with specified first name, last name, and salary.
    /// </summary>
    /// <param name="firstname">First name of the teacher.</param>
    /// <param name="lastname">Last name of the teacher.</param>
    /// <param name="salary">Salary of the teacher.</param>
    public Teacher(string firstname, string lastname, int salary) : base(firstname, lastname)
    {
        Salary = salary;
    }

    /// <summary>
    /// Adds this teacher instance to the database.
    /// </summary>
    public void Add()
    {
        using (var db = new DatabaseContext())
        {
            db.Teachers.Add(this);
            db.SaveChanges();
        }
    }

    /// <summary>
    /// Loads all activities related to this teacher from the database.
    /// </summary>
    public void LoadActivities()
    {
        using (var db = new DatabaseContext())
        {
            Activities = db.Activities.Where(a => a.Teacher.ID == ID).ToList();
        }
    }

    /// <summary>
    /// Returns a string that represents the current teacher.
    /// </summary>
    /// <returns>A string that represents the current teacher.</returns>
    public override string ToString()
    {
        return String.Format("{0} ({1} {2})", Firstname, Lastname, Salary);
    }
}


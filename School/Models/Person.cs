namespace School.Models;
/// <summary>
/// Represents a person with basic properties like first name and last name.
/// This class serves as a base class for other more specific person types.
/// </summary>
public class Person
{
    /// <summary>
    /// Gets the unique identifier for the person.
    /// </summary>
    public Guid ID { get; private set; }

    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public string Firstname { get; set; }

    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    public string Lastname { get; set; }

    /// <summary>
    /// Initializes a new instance of the Person class with specified first and last names.
    /// </summary>
    /// <param name="firstname">The first name of the person.</param>
    /// <param name="lastname">The last name of the person.</param>
    public Person(string firstname, string lastname)
    {
        Firstname = firstname;
        Lastname = lastname;
    }
}

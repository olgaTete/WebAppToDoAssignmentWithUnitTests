using System.ComponentModel.DataAnnotations;

namespace WebAppToDoAssignment.Models
{
    public class Person
    {
        [Key]
        public int id { get; private set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        // Parameterless constructor
        public Person() { }

        // Constructor with parameters
        public Person(int id, string firstName, string lastName)
        {
            // Use this to reference the class-level fields
            this.id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        // Public property for Id (readonly)
        public int Id
        {
            get { return id; }
        }

        // Public property for FirstName with validation
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("First name cannot be null or empty.");
                }
                firstName = value;
            }
        }

        // Public property for LastName with validation
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Last name cannot be null or empty.");
                }
                lastName = value;
            }
        }
    }
}

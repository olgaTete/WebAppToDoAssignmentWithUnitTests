using System.ComponentModel.DataAnnotations;

namespace WebAppToDoAssignment.Models
{
    public class Todo
    {
        [Key]
        public int id { get; private set; }
        public string description { get; set; }
        public bool done { get; set; }
        public Person assignee { get; set; }

        // Parameterless constructor
        public Todo() { }

        // Constructor with parameters
        public Todo(int id, string description)
        {
            this.id = id;
            Description = description;  
            Done = false;               
            Assignee = null;            
        }
        public int Id
        {
            get { return id; }
        }
       
        public string Description
        {
            get { return description; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Description cannot be null or empty.");
                }
                description = value;
            }
        }
        public bool Done
        {
            get { return done; }
            set { done = value; }
        }

        public Person Assignee
        {
            get { return assignee; }
            set { assignee = value; }
        }
    }
}

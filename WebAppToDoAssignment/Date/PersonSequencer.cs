namespace WebAppToDoAssignment.Date
{
    public class PersonSequencer
    {
        private static int personId = 0;

        public static int NextPersonId()
        {
            return ++personId;
        }

        public static void Reset()
        {
            personId = 0;
        }

    }
}

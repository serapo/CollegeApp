namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student
            {
                Id=1,
                StudentName= "Student 1",
                Email = "studentemail@gmail.com",
                Address=    "Hyd, INDIA",
            },
            new Student
            {
                Id=2,
                StudentName= "Student 1",
                Email = "studentemail@gmail.com",
                Address=    "Hyd, INDIA",
            },

             new Student
            {
                Id=3,
                StudentName= "Student 1",
                Email = "studentemail@gmail.com",
                Address=    "Hyd, INDIA",
            },
               new Student
            {
                Id=4,
                StudentName= "Vera",
                Email = "studentemail@gmail.com",
                Address=    "Hyd, INDIA",
            },
        };
    }
}

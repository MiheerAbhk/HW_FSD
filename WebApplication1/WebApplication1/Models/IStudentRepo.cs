﻿using System.Collections.Generic;
namespace WebApplication1.Models
{
    public interface IStudentRepo
    {
        Student GetStudentById(int id);
        List<Student> GetAllStudents();

        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);


    }
}

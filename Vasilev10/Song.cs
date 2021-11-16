using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vasilev10
{
    internal class Song
    {
        string name; //название песни
        string author; //автор песни
        Song prev; //связь с предыдущей песней в списке
        
        public void setName(string Name) //метод для заполнения поля name
        {
            name = Name;
        }

        public void setAuthor(string Author) //метод для заполнения поля author
        {
            author = Author;
        }
        public void setPrev(Song song) //метод для заполнения поля prev
        {
            prev = song;
        }
              
        public string Title() //метод для печати названия песни и ее исполнителя
        {
            return $"{name} - {author}";
        }

        public override bool Equals(Object d) //метод, который сравнивает между собой два объекта-песни
        {
            if (d.GetType() == GetType())
                return true;
            else
                return false;
        }
    }
}

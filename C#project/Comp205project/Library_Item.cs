using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp205project
{
     public abstract class Library_Item
    {
         private int myNumOfCopies;	    //number of copies
         private int myCopyAvailable;	//number of copies available 
         private int myCallNumber;      //the call number

         //abstract method to provide the item's detail
         public abstract string itemDetail();

         //constructor that accepts the number of copies and call number 
         public Library_Item(int ncopy, int cnumber)
         {
         myNumOfCopies = ncopy;
         myCopyAvailable = ncopy;
         myCallNumber = cnumber;
         }

         //readonly public property for number of copies
         public int NumCopy
         { get { return myNumOfCopies; } }

         //readonly public property for number of copies avilable
         public int NumCopyAvailable
         { get { return myCopyAvailable; } }

         //readonly public property for call number
         public int CallNum
         { get { return myCallNumber; } }

         //the ToString() method
         public override string ToString()
         {
             return myCallNumber.ToString();
         }

         //the method isAvailable() to check if there are copies avilable
         public bool isAvailable()
         {
             if (myCopyAvailable > 0)
                 return true;
             else
                 return false;
         }

         //the method decreaseCopy() to decrease the number of copies available
         public void decreaseCopy()
         {
             myCopyAvailable--;
         }

         //the method increaseCopy() to increase the number of copies avilable
         public void increaseCopy()
         {
             myCopyAvailable++;
         }
    }



     public class Book : Library_Item   //Book subclass of Library_Item
     {
         private string myTitle;        //Book's title
         private string myAuthor;       //Book's author

         //constructor for Book class
         public Book(int ncopy, int callnum, string title, string author)
             : base(ncopy, callnum)
         {
             myTitle = title;
             myAuthor = author;
         }

         //readonly public property for title
         public string Title
         {
             get { return myTitle; }
         }

         //readonly public property for author
         public string Author
         {
             get { return myAuthor; }
         }

         //the ToString() method
         public override string ToString()
         {
             return base.ToString() + " " + myTitle;
         }

         //the method itemDetail()for Book
         public override string itemDetail()
         {
             return "Type: Book\n" + " Title:  " + myTitle+ "\nCall Number:  " + CallNum + "\nNumber of Copies: " + NumCopy + "\nCopies Available: " + NumCopyAvailable;
         }

     }



     public class Journal : Library_Item    //Journal subclass of Library_Item
     {
         private string myTitle;            //Journal's title
         private int myVolumeNumber;        //journal's volume number

         //constructor for Journal class
         public Journal(int ncopy, int callnum, string title, int vol)
             : base(ncopy, callnum)
         {
             myTitle = title;
             myVolumeNumber = vol;
         }

         //readonly public property for title
         public string Title
         {
             get { return myTitle; }
         }

         //readonly public property for volum number
         public int VolumeNumber
         {
             get { return myVolumeNumber; }
         }

         //the ToString() method
         public override string ToString()
         {
             return base.ToString() + " " + myTitle + " Vol: " + myVolumeNumber;
         }


         //the method itemDetail()for Journal
         public override string itemDetail()
         {
             return "Type: Journal\n" + "Title: " + myTitle + "\nCall Number: " + CallNum + "\nNumber of Copies: " + NumCopy +
                 "\nCopies Available: " + NumCopyAvailable;
         }


     }


}

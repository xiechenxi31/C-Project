using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp205project
{
    public abstract class Library_Member
    {
        private int myMemberID;              //member’s ID
        private string myMemberName;         //member's name
        protected int myBooks;               //number of books borrowed
        protected List<Loan> myItems;        //list of borrowed items
        private static int nextID = 1000;    //variable used to generate unique ID

        //abstract method to check whether member is allowed to borrow library item
        public abstract bool CanBorrow(Library_Item li);

        //abstract method to add borrowed item to the list
        public abstract void addBorrowedItem(Loan bItem);

        //abstract method to remove borrowed item from the list
        public abstract void removeBorrowedItem(Library_Item bItem);

        public Library_Member()
        {
            //set the properties accordingly
            myItems = new List<Loan>();
            myMemberID = nextID++;

        }

        public Library_Member(string name)
            : this()
        {
            //set the mumber name
            myMemberName = name;
        }

        public int MemberID
        {
            //ID cannot be changed but it can use
            get { return myMemberID; }

        }

        public string MemberName
        {
            //membername can use and change
            get { return myMemberName; }
            set { myMemberName = value; }
        }

        public List<Loan> ItemsBorrowed
        {
            //list of items borrowed cannot be changed but it can use
            get { return myItems; }
            
        }


        public int BookCount
        {
            //bookcount can use and change
            get { return myBooks; }
            set { myBooks = value; }

        }

        public override string ToString()
        {
            //method to return a string that contains member’s id and name
            return "MemberID: " + Convert.ToString(myMemberID) + " Member Name: " + myMemberName;

        }

        //method to find the library item from the list of borrowed items
        public Loan findBorrowedItem(Library_Item li)
        {
            Loan aitem = null;
            foreach (Loan theitem in myItems)
            {
                if (theitem.Item.Equals(li))
                    aitem = theitem;
            }
            return aitem;
        }
    }



    public class Faculty_Member : Library_Member
    {
        private string myPosition;                   //the position
        private int myJournals;                      //number of journals borrowed
        private static int FacBookMax = 4;           //maximum number of books allowed
        private static int FacJournalMax = 2;        //maximum number of journals allowed

        public Faculty_Member(string name, string pos)
            : base(name)
        {
            //set faculty member's position
            myPosition = pos;
        }

        public string Position
        {
            //position can use and change
            get { return myPosition; }
            set { myPosition = value; }

        }

        public int BooksCount
        {
            //staff bookcount can use and change
            get { return myBooks; }
            set { myBooks = value; }
        }

        public int JournalsCount
        {
            //staff journalscount can use and change
            get { return myJournals; }
            set { myJournals = value; }
        }

        public override bool CanBorrow(Library_Item li)
        {
            //method to check whether faculty member can borrow library item
            bool borrow = false;

            //library item could be book or journal, need to get type of a certain item.
            if (li.GetType() == typeof(Book))
            {
                // staff can only borrow up to four books and the item must available.
                if (BooksCount <= FacBookMax && li.isAvailable())
                {
                    borrow = true;
                }
            }

            if (li.GetType() == typeof(Journal))
            {
                //staff can only borrow up to two journals and the journal must available.
                if (JournalsCount <= FacJournalMax && li.isAvailable())
                {
                    borrow = true;
                }
            }

            return borrow;
        }

        public override void addBorrowedItem(Loan bItem)
        {
            //method to add borrowed item to the list

            ItemsBorrowed.Add(bItem);

        }

        public override void removeBorrowedItem(Library_Item bItem)
        {
            //method to remove borrowed item from the list
            ItemsBorrowed.Remove(findBorrowedItem(bItem));

        }

        public override string ToString()
        {
            //method to return a string about a member's basic information
            string staffdetail;
            staffdetail = MemberID.ToString() + " " + MemberName + " " + "Staff" + " \n " + "" + "\n";
            //find out all items the member is borrowing
            foreach (Loan a in ItemsBorrowed)
            {
                staffdetail += a.ToString() + "\n";
            }

            return staffdetail;
        }

    }


    public class Student : Library_Member
    {
        private string myMajor;                       //student’s major
        private string myPhone;                       //student’s phone number

        private static int StudentBookMax = 2;        //maximum number of books allowed

        public Student(string name, string major, string ph)
            : base(name)
        {
            //set the student major and phone number
            myMajor = major;
            myPhone = ph;
        }

        public string Major
        {
            //major can use and change
            get { return myMajor; }
            set { myMajor = value; }
        }

        public string Phone
        {
            //phone number can use and change
            get { return myPhone; }
            set { myPhone = value; }
        }


        public override bool CanBorrow(Library_Item li)
        {
            //method to check whether student can borrow library item
            bool borrow = false;
            if (li.GetType() == typeof(Book))
            {
                //student can only borrow up to two book and it must be available
                if (BookCount <= StudentBookMax && li.isAvailable())
                {
                    borrow = true;
                }

            }
            return borrow;


        }

        public override void addBorrowedItem(Loan bItem)
        {
            //method to add borrowed item to the list
            ItemsBorrowed.Add(bItem);
        }

        public override void removeBorrowedItem(Library_Item bItem)
        {
            //method to remove borrowed item
            ItemsBorrowed.Remove(findBorrowedItem(bItem));
        }

        public override string ToString()
        {
            //return student's basic information
            string studentdetail = "";
            studentdetail = MemberID.ToString() + " " + MemberName + " " + "Student" + "\n" + "" + "\n";  
            //return all borrowed item's information
            foreach (Loan a in ItemsBorrowed)
            {
                studentdetail += a.ToString() + "\n";
            }

            return studentdetail;
        }

        }

    }


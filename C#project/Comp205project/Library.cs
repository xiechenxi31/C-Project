using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp205project
{
    public class Library
    {
        // Controller class
        private List<Library_Member> myMembers;                   //references to all the library members
        private List<Library_Item> myItems;                       //references to all library items

        public Library()
        {
            //set the construct
            myMembers = new List<Library_Member>();
            myItems = new List<Library_Item>();
        }

        public List<Library_Member> Members
        {
            //return the list of members and it can use and change
            get { return myMembers; }
            set { myMembers = value; }
        }

        public List<Library_Item> Items
        {
            //return the list of items and it can use and change
            get { return myItems; }
            set { myItems = value; }
        }

        public Library_Item findItem(int callnum)
        {
            //find an item based on call number
            Library_Item anitem = null;
            //if two call number equals then it is the same one
            foreach (Library_Item theitem in myItems)
            {
                if (theitem.CallNum.Equals(callnum))
                    anitem = theitem;
            }
            return anitem;

        }

        public Library_Member findMember(int idnum)
        {
            //find a member based on member id
            Library_Member amember = null;
            // if two id number same then it is the same member
            foreach (Library_Member themember in myMembers)
            {
                if (themember.MemberID.Equals(idnum))
                    amember = themember;
            }
            return amember;
        }

        public void addBookItem(int ncopy, int callnum, string title, string author)
        {
            //add book to the items list
            myItems.Add(new Book(ncopy, callnum, title, author));

        }

        public void addJournalItem(int ncopy, int callnum, string title, int vol)
        {
            //add journal to the items list
            myItems.Add(new Journal(ncopy, callnum, title, vol));
            new Journal(ncopy, callnum, title, vol);

        }

        public void addStudent(string name, string major, string ph)
        {
            //add student to the members list
            myMembers.Add(new Student(name, major, ph));
        }

        public void addStaff(string name, string pos)
        {
            //add staff to the members list
            myMembers.Add(new Faculty_Member(name, pos));
        }

        public string borrow(int callnum, int idnum)
        {
            //define item, member and use find method to find it.
            Library_Item anitem;
            anitem = findItem(callnum);
            Library_Member amember;
            amember = findMember(idnum);

            // borrow method if the item is a book
            if (anitem.GetType() == typeof(Book))
            {
                //borrow method if the member is a faculty member
                if (amember.GetType() == typeof(Faculty_Member))
                {
                    Faculty_Member fmember;
                    fmember = (Faculty_Member)amember;
                    fmember.BooksCount++;
                    if (fmember.CanBorrow(anitem))
                    {
                        Loan staffborrow = new Loan(anitem, DateTime.Today, DateTime.Today.AddDays(14));
                        amember.addBorrowedItem(staffborrow);
                        anitem.decreaseCopy();
                        myMembers.Add(amember);
                        return "The Faculty Member Can Borrow The Book";
                    }
                }
                else
                {
                    //borrow method if the member is a student member
                    Student astudent;
                    astudent = (Student)amember;
                    astudent.BookCount++;
                    if (astudent.CanBorrow(anitem))
                    {
                        Loan studentborrow = new Loan(anitem, DateTime.Today, DateTime.Today.AddDays(14));
                        amember.addBorrowedItem(studentborrow);
                        anitem.decreaseCopy();
                        myMembers.Add(amember);
                        return "The student Member Can Borrow The Book";
                    }
                }
            }
                //borrow method if the item is a journal and the member is a faculty member
            else if (amember.GetType() == typeof(Faculty_Member))
            {
                //addBorrowedItem(Loan bItem)
                Faculty_Member afmember;
                afmember = (Faculty_Member)amember;
                afmember.JournalsCount++;
                if (afmember.CanBorrow(anitem))
                {
                    Loan staffborrowed = new Loan(anitem, DateTime.Today, DateTime.Today.AddDays(14));
                    amember.addBorrowedItem(staffborrowed);
                    anitem.decreaseCopy();
                    myMembers.Add(amember);
                    return "The Faculty Member Can Borrow The Journal";
                }
            }
                //warning message if a studnet trying to borrow a journal
            else return "The Student Cannot Borrow The Journal"; ;

            // warning message if a member cannot borrow anymore item or the item is not avaliable
            return " Sorry You Cannot Borrow It ";
        }



        public string borrow(int callnum, int idnum, DateTime aDate)
        {
            //this method is only use for read the loan.txt
            Library_Item anitem;
            anitem = findItem(callnum);
            Library_Member amember;
            amember = findMember(idnum);

            if (anitem.GetType() == typeof(Book))
            {
                if (amember.GetType() == typeof(Faculty_Member))
                {
                    Faculty_Member afmember;
                    afmember = (Faculty_Member)amember;
                    afmember.BooksCount++;
                    if (afmember.CanBorrow(anitem))
                    {
                        // add the history data
                        Loan astaffborrow = new Loan(anitem, aDate, aDate.AddDays(14));
                        afmember.addBorrowedItem(astaffborrow);
                        anitem.decreaseCopy();
                        myMembers.Add(amember);
                        return "The Faculty Member borrowed the book  on " + " " + Convert.ToString(aDate);
                    }
                    else
                    {
                        Student astudent;
                        astudent = (Student)amember;
                        astudent.BookCount++;
                        if (astudent.CanBorrow(anitem))
                        {
                            // add the history data
                            Loan studentborrow = new Loan(anitem, aDate, aDate.AddDays(14));
                            amember.addBorrowedItem(studentborrow);
                            anitem.decreaseCopy();
                            myMembers.Add(amember);
                            return "The student member borrowed the book on" + " " + Convert.ToString(aDate);
                        }
                    }
                }
            }
            else
            {
                if (amember.GetType() == typeof(Faculty_Member))
                {
                    //addBorrowedItem(Loan bItem)
                    Faculty_Member afmember;
                    afmember = (Faculty_Member)amember;
                    afmember.JournalsCount++;
                    if (afmember.CanBorrow(anitem))
                    {
                        //add history data
                        Loan staffborrowed = new Loan(anitem, aDate, aDate.AddDays(14));
                        amember.addBorrowedItem(staffborrowed);
                        anitem.decreaseCopy();
                        myMembers.Add(amember);
                        return "The Faculty Member borrowed the Journal  on " + " " + Convert.ToString(aDate);
                    }
                }
                else return " "; ;
            }
            return "  ";
        }

        public string returnItem(int callnum, int idnum)
        {
            //method to return an item first define variable
            string returninfo = " ";
            Library_Item anitem;
            anitem = findItem(callnum);
            Library_Member amember;
            amember = findMember(idnum);

            //method for faculty member
            if (amember.GetType() == typeof(Faculty_Member))
            {
                //define faculty variable
                Faculty_Member fmember;
                fmember = (Faculty_Member)amember;

                //method if the member borrowed the item
                if (fmember.findBorrowedItem(anitem) != null)
                {
                    //find the loan
                    Loan theloan;
                    theloan = fmember.findBorrowedItem(anitem);

                    //method to find the due date and calculate the number of days that overdue
                    DateTime thedue, now;
                    thedue = theloan.DueDate;
                    now = DateTime.Today;
                    int comparetime;
                    comparetime = DateTime.Compare(thedue, now);
                    TimeSpan difference = now - thedue;
                    var days = difference.TotalDays;

                    //remove the item from the list and increase copy number
                    fmember.removeBorrowedItem(anitem);
                    anitem.increaseCopy();
                    if (anitem.GetType() == typeof(Book))
                    {
                        fmember.BooksCount--;
                        
                        // show the information that if the item is overdue
                        if (comparetime == -1)
                        {
                            Book abook;
                            abook = (Book)anitem;
                            returninfo = "Library Member: " + amember.MemberName + "\n" + "Library Item: " + abook.Title + "\n" + "Due Date: " + Convert.ToString(thedue)
                                + "\n" + "Date Returned: " + Convert.ToString(now) + "\n" + "Days Overdue: " + Convert.ToString(days) + "\n"
                                + "Fine: " + Convert.ToString(days * 0.5);
                        }
                            //if the item is not overdue
                        else returninfo = "Return Completed";
                    }
                    else
                    {
                        fmember.JournalsCount--;
                        // show the information that if the item is overdue
                        if (comparetime == -1)
                        {
                            Journal ajournal;
                            ajournal = (Journal)anitem;
                            returninfo = "Library Member: " + amember.MemberName + "\n" + "Library Item: " + ajournal.Title + "\n" + "Due Date: " + Convert.ToString(thedue)
                                + "\n" + "Date Returned: " + Convert.ToString(now) + "\n" + "Days Overdue: " + Convert.ToString(days) + "\n"
                                + "Fine: " + Convert.ToString(days* 0.5);
                        }
                        //if the item is not overdue
                        else returninfo = "Return Completed";
                    }
                }
                    //if the member did not borrow the item
                else returninfo = "The member did not borrow this item";
            }
                //method for student member
            else
            {
                Student astudent;
                astudent = (Student)amember;

                //method if the member borrowed the item and the student can only borrow book, so we do not need to worry about journal
                if (astudent.findBorrowedItem(anitem) != null)
                {
                    //find the loan
                    Loan theloan;
                    theloan = astudent.findBorrowedItem(anitem);

                    //method to find the due date and calculate the number of days that overdue
                    DateTime thedue, now;
                    thedue = theloan.DueDate;
                    now = DateTime.Today;
                    int comparetime;
                    comparetime = DateTime.Compare(thedue, now);
                    TimeSpan difference = now - thedue;
                    var days = difference.TotalDays;

                    //remove the item from the list, decrease the member's bookcount and increase the item's copy number
                    astudent.removeBorrowedItem(anitem);
                    astudent.BookCount--;
                    anitem.increaseCopy();

                    // show the information that if the item is overdue
                    if (comparetime == -1)
                    {
                        Book asbook;
                        asbook = (Book)anitem;
                        returninfo = "Library Member: " + amember.MemberName + "\n" + "Library Item: " + asbook.Title + "\n" + "Due Date: " + Convert.ToString(thedue)
                            + "\n" + "Date Returned: " + Convert.ToString(now) + "\n" + "Days Overdue: " + Convert.ToString(days) + "\n"
                            + "Fine: " + Convert.ToString(Convert.ToInt32(days* 0.5));
                    }

                    //if the item is not overdue
                    else returninfo = "Return Completed";
                    }

                    //if the member did not borrow the item
                else returninfo = "The member did not borrow this item";
            }
            return returninfo;
        }


        public string itemDetail(int callnum)
        {
            //method to process detail information about an item based on call number
            Library_Item anitem;
            anitem = findItem(callnum);
            string iteminfo = " ";

            // find item type and call the revelate method to get string

            if (anitem.GetType() == typeof(Book))
            {
                Book abook;
                abook = (Book)anitem;
                iteminfo = abook.itemDetail();

            }
            else
            {
                Journal ajournal;
                ajournal = (Journal)anitem;
                iteminfo = ajournal.itemDetail();
            }
            return iteminfo;

        }

        public string memberDetail(int idnum)
        {
            //method to process detail information about a member based on member id
            //method also displays all items borrowed by this member
            Library_Member amember;
            amember = findMember(idnum);
            string memberinfo = " ";

            //find member info base on the member type and call the revelate method to get string
            if (amember.GetType() == typeof(Faculty_Member))
            {
                Faculty_Member fmember;
                fmember = (Faculty_Member)amember;
                memberinfo = fmember.ToString();
            }
            else
            {
                Student astudent;
                astudent = (Student)amember;
                memberinfo = astudent.ToString();
            }

            return memberinfo;
        }


    }
}

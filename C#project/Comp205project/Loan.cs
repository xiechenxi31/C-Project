using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comp205project
{
    public class Loan
    {
        private Library_Item myItem;                                  //library item on loan
        private DateTime myDateBorrowed;                              //date the item was borrowed
        private DateTime myDueDate;                                   //the item’s due date

        public Loan(Library_Item li, DateTime bdate, DateTime rdate)
        {
            //set the construct
            myItem = li;
            myDateBorrowed = bdate;
            myDueDate = rdate;
        }

        public Library_Item Item
        {
            //item can change and use
            get { return myItem; }
            set { myItem = value; }
        }

        public DateTime DateBorrowed
        {
            get { return myDateBorrowed; }
            set { myDateBorrowed = value; }
        }

        public DateTime DueDate
        {
            //due date can change and use
            get { return myDueDate; }
            set { myDueDate = value; }
        }

        public override string ToString()
        {
            //return information about each loan's information
            string loaninfo;

             if (Item.GetType() == typeof(Book))
             {
                 Book abook;
                 abook = (Book)Item;
                 loaninfo = Convert.ToString(myItem.CallNum) + " " + abook.Title + " " + DateBorrowed.ToShortDateString() + " " +DueDate.ToShortDateString();
             }
             else
             {
                 Journal ajournal;
                 ajournal = (Journal)Item;
                 loaninfo = Convert.ToString(myItem.CallNum) + " " + ajournal.Title + " " + Convert.ToString(ajournal.VolumeNumber) + " " + DateBorrowed.ToShortDateString() + " " + DueDate.ToShortDateString();
             }

            return loaninfo;
        }
    }
}

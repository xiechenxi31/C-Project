using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Comp205project
{
    public partial class Form1 : Form
    {
        public Library alibrary = new Library();
        public string themember;
        public string theitem;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            readmemberlist();
            readitemlist();
            displayList();
            readloanlist(); 
        }

        //add information to the list box
        public void displayList()
        {
            foreach (Library_Member m in alibrary.Members)
                lstmember.Items.Add(m);
            foreach (Library_Item a in alibrary.Items)
                lstitems.Items.Add(a);
        }

        public void readmemberlist()
        {
            //read information about members from file "LibMember.txt"
            //creates member objects and adds to the list

            string[] memberinfoarray;
            string memberinfo;
            char chrsep = Convert.ToChar(",");
            StreamReader myfilereader;

            // Tell the stream reader what it is reading
            myfilereader = new StreamReader("LibMember.txt");

            //read each line until there are no more lines
            while (!myfilereader.EndOfStream)
            {
                memberinfo = myfilereader.ReadLine();
                //split the strings
                memberinfoarray = memberinfo.Split(chrsep);
                //create a new member object
                //find out is a student or faculty member add them to the member list
                if (memberinfoarray[0] == "A")
                {
                    alibrary.addStaff(memberinfoarray[1], memberinfoarray[2]);
                }
                else
                {
                    alibrary.addStudent(memberinfoarray[1], memberinfoarray[2], memberinfoarray[3]);
                }
            }
        }

        public void readitemlist()
        {
            //read information about members from file "LibItem.txt"
            //creates member objects and adds to the list


            string[] iteminfoarray;
            string iteminfo;
            char chrsep = Convert.ToChar(",");
            StreamReader myfilereaderitem;

            // Tell the stream reader what it is reading
            myfilereaderitem = new StreamReader("LibItem.txt");

            //read each line until there are no more lines
            while (!myfilereaderitem.EndOfStream)
            {
                iteminfo = myfilereaderitem.ReadLine();
                //split the strings
                iteminfoarray = iteminfo.Split(chrsep);
                //create a new item object
                //find out is a book or Journal and add it to the item list
                if (iteminfoarray[0]=="B")
                {
                    alibrary.addBookItem(Convert.ToInt32(iteminfoarray[1]), Convert.ToInt32(iteminfoarray[2]), iteminfoarray[3], iteminfoarray[4]);
                }
                else
                {
                    alibrary.addJournalItem(Convert.ToInt32(iteminfoarray[1]), Convert.ToInt32(iteminfoarray[2]), iteminfoarray[3], Convert.ToInt32(iteminfoarray[4]));
                }

            }



        }
        public void readloanlist()
        {
            //read information about members from file "Loans.txt"

            string[] loaninfoarray;
            string loaninfo;
            char chrsep = Convert.ToChar(",");
            StreamReader myfilereaderitem;

            // Tell the stream reader what it is reading
            myfilereaderitem = new StreamReader("Loans.txt");

            //read each line until there are no more lines
            while (!myfilereaderitem.EndOfStream)
            {
                //split the strings
                loaninfo = myfilereaderitem.ReadLine();
                loaninfoarray = loaninfo.Split(chrsep);

                // add the data to the loan list
                alibrary.borrow(Convert.ToInt32(loaninfoarray[0]), Convert.ToInt32(loaninfoarray[1]), Convert.ToDateTime(loaninfoarray[2]));
            }
        }

        //when return button clicked
        private void button2_Click(object sender, EventArgs e)
        {
            int idnumber;
            int callnumber;
            string[] borrowinfoarray;
            string borrowinfo = lstmember.SelectedItem.ToString();
            char chrsep = Convert.ToChar(" ");

            //split the strings
            borrowinfoarray = borrowinfo.Split(chrsep);

            //get the idnumber from the selected one
            idnumber = Convert.ToInt32(borrowinfoarray[0]);


            //lstitems.SelectedItem.ToString();

            string[] callinfoarray;
            string callinfo = lstitems.SelectedItem.ToString();

            //split the strings
            callinfoarray = callinfo.Split(chrsep);

            //get the callnumber from the selected one
            callnumber = Convert.ToInt32(callinfoarray[0]);

            //call the return method from the control class and show the information
            MessageBox.Show(alibrary.returnItem(callnumber, idnumber));
            
        }


        // when borrow button clicked
        private void button1_Click(object sender, EventArgs e)
        {
            //lstmember.SelectedItem.ToString();
            int idnumber;
            int callnumber;
            string[] borrowinfoarray;
            string borrowinfo = lstmember.SelectedItem.ToString();
            char chrsep = Convert.ToChar(" ");

            //split the strings
            borrowinfoarray = borrowinfo.Split(chrsep);


            //get the idnumber from the select one
            idnumber = Convert.ToInt32(borrowinfoarray[0]);

            string[] callinfoarray;
            string callinfo = lstitems.SelectedItem.ToString();

            //split the strings
            callinfoarray = callinfo.Split(chrsep);

            //get the callnumber from the select one
            callnumber = Convert.ToInt32(callinfoarray[0]);

            //call the borrow method from the control class and show the information
            MessageBox.Show(alibrary.borrow(callnumber,idnumber) );

        }


        //when exit button clicked
        private void button5_Click(object sender, EventArgs e)
        {
            //close the app
            this.Close();
        }


        //when memberinfo button clicked
        private void button3_Click(object sender, EventArgs e)
        {
            int idnumber;
            string[] borrowinfoarray;
            string borrowinfo = lstmember.SelectedItem.ToString();
            char chrsep = Convert.ToChar(" ");

            //split the strings
            borrowinfoarray = borrowinfo.Split(chrsep);

            //get the idnumber from the selected one
            idnumber = Convert.ToInt32(borrowinfoarray[0]);

            //call the memberinfo method from the control class and show the information in label3
            label3.Text=(alibrary.memberDetail(idnumber));
        }


        //when iteminfo button clicked
        private void button4_Click(object sender, EventArgs e)
        {
            int callnumber;
            string[] callinfoarray;
            string callinfo = lstitems.SelectedItem.ToString();
            char chrsep = Convert.ToChar(" ");

            //split the strings
            callinfoarray = callinfo.Split(chrsep);

            // get the callnumber from the selected one
            callnumber = Convert.ToInt32(callinfoarray[0]);


            //call the iteminfo method from the control class and show the information in label3
            label3.Text=(alibrary.itemDetail(callnumber));
        }


        
    }
}

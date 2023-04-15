﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using Newtonsoft.Json;
using dmdiApp;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    

    public class JournalEntry
    {
        public string ProductName { get; set; }

        public string ProductAccount { get; set; }

        public string TransactionType { get; set; }

        public Decimal Amount { get; set; }

        public string Branch { get; set; }

        public string TellerAccount { get; set; }

   }
    public partial class MainWindow : Window
    {
        public static Hashtable loanProductsNew = new Hashtable(){
                      {"TRACLNEQR","Trade & Comm Loans, Trade & Comm Loans"},
                      {"TRACLN-W","Trade & Comm Loans, Trade & Comm Loans"},
                      {"SERVLNEQR", "Service Loans, Service Loans"},
                      {"SERVLN-W", "Service Loans, Service Loans"},
                      {"PRODLNUSDEQR","Production Loans, Production Loans"},
                      {"PRODNL-W","Production Loans, Production Loans"},
                      {"CONSULNEQR","Consumer Loans, Consumer Loans"},
                      {"AGRICLSWA","Agric Loans, Agric  Loan- Receivable"},
                      {"PHFLN","Private Home Financing, Private Home Finan Loans"},
                      {"SAVLDLN", "Savings Led, Savings Led"},
                      {"RESTRUCTUREAL",",Restructured Loans"},
                      {"STAFFL","Staff Housing Loan - USD,"},
                      {"ALSCF1",""}
                   };
        Hashtable depositProdsMapNew = new Hashtable(){
                      {"SVGCONLN","Collateral Savings, ''"},
                      {"SVGSERLN","Collateral Savings, ''"},
                      {"SVGSERLN - W","Collateral Savings, ''"},
                      {"SVGTRCLN", "Collateral Savings, ''"},
                      {"LRDREG", "Regular Savings, ''"},
                      {"SSD", "Regular Savings, ''"},
                      {"SSW","Regular Savings, ''"},
                      {"SSM","Regular Savings, ''"},
                      {"REGSAVG","Regular Savings,''"},
                      {"SVGPPHFLN","Collateral Savings,''"},
                      {"SVGPPRIHLN","Collateral Savings, ''"},
                      {"SVGPRDLN","Collateral Savings,''"},
                      {"SVGPRODLN - W","Collateral Savings,''"},
                      {"SVGTRDCOM","Collateral Savings,''"},
                      {"CSA","CLIENTS SAVINGS,''"},
                      {"SVGSTAFF","Regular Savings,''"},
                      {"SVGLED","Regular Savings,''"},
                      {"TERMDEP","Term Deposits,''"}
                   };

        /// <summary>
        /// these varaibles determine whether to debit of credit the product account
        /// i.e it only applies the action that is true. If prodAccntDebit is true a debit is applied to the product accnt
        /// if prodAccntCredit is true a Credit is applied to the product accnt
        /// </summary>
        public static bool? prodAccntDebit;
        public static bool? prodAccntCredit;
        public static bool usdDownload = false;
        public static bool lrdDownload = false;

        /// <summary>
        /// these variables determine wether to debit or credit the teller account 
        /// i.e it only applies the action that is true. If tellerAccntDebit is true a debit is applied to the Teller accnt
        /// if tellerAccntCredit is true a Credit is applied to the teller accnt
        /// </summary>
        public static bool? tellerAccntDebit;
        public static bool? tellerAccntCredit;

        //this is the private variable holds a list of transaction objects
        private static List<JournalEntry> journalEntries; 

        //this is the public variable holds a list of transaction objects
        public static List<JournalEntry> JournalEntries
        {
            get { return journalEntries;  }
            set { journalEntries = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            winmain.Title = "Instafin-Quickbooks Integration Application";

            ////List<Transaction> txn = new List<Transaction>();
            ////initialize the list of transaction objects.
            
           // journalentries.DataContext = JournalEntries;
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //set the download type
            MainWindow.JournalEntries = new List<JournalEntry>();
            if(usdDownload is true)
            {
                usdDownload = false;
            }
            lrdDownload = true;
            vaulttransfer.Content = " Transfer LRD To Vault ";

            //e.OriginalSource
            btnlrd.Content = " Downloading ... ";
            dmdiApp.dmditxn lrdTxn = new dmdiApp.dmditxn();

            //this is the date for which transactions are requested
            DateTime? txnDate = txndateCtl.SelectedDate;

            try
            {
                if (txnDate is null)
                {
                    throw new Exception("You must select a valid transaction date on the calendar!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Instafin-Quickbooks Integration", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //convert the nullable txnDate to a proper DateTime object
            DateTime tdate = Convert.ToDateTime(txnDate);

            string day = tdate.Day.ToString();
            string dayOfWeek = tdate.DayOfWeek.ToString();
            string month = tdate.Month.ToString();
            string year = tdate.Year.ToString();

            string fullDate = year + "-" + month + "-" + day;
            string txndate = month + "/" + day + "/" + year;

            //if the day is Sunday throw and Error
            try
            {
                if (dayOfWeek == "Sunday")
                {
                    throw new Exception(dayOfWeek + " is not a valid transaction day!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Instafin-Quickbooks Integration", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            //var jsonData = lrdTxn.getJsonData();

            var jsonData = lrdTxn.getTxnLDLive(fullDate);
            //sdailyTxn.Text = txn_string;

            var txns = JsonConvert.DeserializeObject<AllTransactions>(jsonData);

            //add to datacontext of the DataGrid
            //TxnList.Add(null);

            //lrdTxn.createJsonFFile(jsonData);

            lrdTxn.LoanProducts_PrincipleList(txns);
            lrdTxn.LoanProducts_InterestList(txns);
            lrdTxn.DisbursementProductsList(txns);
            lrdTxn.SavingsDepositProductsList(txns);
            lrdTxn.WithdrawalDepositProductsList(txns);

            //lrdTxn.calcLoanProducts_Principle(txns,txndate);
            //lrdTxn.calcLoanProducts_Interests(txns);

            //lrdTxn.calcDisbursementProducts(txns);
            //lrdTxn.calcSavingsDepositProducts(txns);
            //lrdTxn.calcWithdrawalDepositProducts(txns);
            MessageBox.Show("Download  Succesful ...", "Instafin-Quickbooks Integration");

           // JournalEntries.Add(new JournalEntry() { ProductName = "Trade&Comm", ProductAccount = "Trade & Commerce Loan", TransactionType = "LoanRepayment", Amount = 57600.07M, Branch = "RedLight", TellerAccount = "Teller 2 Redlight(LRD)" });
          //  JournalEntries.Add(new JournalEntry() { ProductName = "ServiceLoan", ProductAccount = "Service Loans", TransactionType = "LoanRepayment", Amount = 157600.07M, Branch = "Monrovia", TellerAccount = "Teller 2 Head Office(LRD)" });
            //JournalEntries.Add(new JournalEntry() { ProductName = "Trade&Comm", ProductAccount = "Trade & Commerce Loan", TransactionType = "Disbursement", Amount = 87600.07M, Branch="Redlight", TellerAccount= "Teller 2 Redlight(LRD" });

            journalentries.ItemsSource = JournalEntries;

            btnlrd.Content = "Download successfull.";


        }

        private void btnusd_Click(object sender, RoutedEventArgs e)
        {
            ///check ifthe Calendar date has been selected
            ///

            MainWindow.JournalEntries = new List<JournalEntry>();

            //set the download type
            if(lrdDownload is true)
            {
                lrdDownload = false;
            }
            usdDownload = true;
            vaulttransfer.Content = " Transfer USD To Vault ";
            btnusd.Content = " Downloading ... ";

            dmditxnusd txnusd = new dmditxnusd();
           // dmdiApp.dmditxn lrdTxn = new dmdiApp.dmditxn();

            DateTime? txnDate = txndateCtl.SelectedDate;

            try
            {       
                if (txnDate is null)
                {
                    throw new Exception("You must select a valid transaction date on the calendar!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Instafin-Quickbooks Integration", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //convert the nullable txnDate o a proper DateTime object
            DateTime tdate = Convert.ToDateTime(txnDate);

            string day = tdate.Day.ToString();
            string dayOfWeek = tdate.DayOfWeek.ToString();
            string month = tdate.Month.ToString();
            string year = tdate.Year.ToString();

            string fullDate = year + "-" + month + "-" + day;
            string txndate = month + "/" + day + "/" + year;

            //if the day is Sunday throw and Error
            try { 
                    if (dayOfWeek == "Sunday")
                    {
                    throw new Exception(day + " is not a valid transaction day!");
                    }
                }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Instafin-Quickbooks Integration", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

           var jsonData = txnusd.getTxnLiveUSD(fullDate);
            //sdailyTxn.Text = txn_string;

           // txnusd.createJsonFFileUSD(jsonData);
           //var jsonData = txnusd.getJsonDataUSD();

            var txns = JsonConvert.DeserializeObject<AllTransactions>(jsonData);

            // txnusd.calcLoanProducts_Principle(txns);
            // txnusd.calcLoanProducts_Interests(txns);

            //  txnusd.calcDisbursementProducts(txns);
            //  txnusd.calcSavingsDepositProducts(txns);
            //  txnusd.calcWithdrawalDepositProducts(txns);
            txnusd.LoanProducts_PrincipleList(txns);
            txnusd.LoanProducts_InterestList(txns);
            txnusd.DisbursementProductsList(txns);
            txnusd.SavingsDepositProductsList(txns);
            txnusd.WithdrawalDepositProductsList(txns);

            journalentries.ItemsSource = JournalEntries;
            MessageBox.Show("Download  Succesful ...", "Instafin-Quickbooks Integration",MessageBoxButton.OKCancel, MessageBoxImage.Information);

            btnusd.Content = "Download complete.";
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            //this is the date for which transactions are requested
            DateTime? txnDate = txndateCtl.SelectedDate;

            //convert the nullable txnDate to a proper DateTime object
            DateTime tdate = Convert.ToDateTime(txnDate);

            string day = tdate.Day.ToString();
            string month = tdate.Month.ToString();
            string year = tdate.Year.ToString();

            string fullDate = year + "-" + month + "-" + day;
            string txndate = month + "/" + day + "/" + year; ;
            dmdiApp.dmditxn lrdTxn = new dmdiApp.dmditxn();
            dmdiApp.dmditxnusd usdTxn = new dmditxnusd();

            // dataGridRow.
            // var dt = journalentries.Columns[3].GetCellContent(;

            try
            {
                if (prodAccntCredit is true && tellerAccntCredit is true)
                { throw new Exception("You cannot credit Product and Teller accounts at the same time"); }

                if (prodAccntDebit is true && tellerAccntDebit is true)
                { throw new Exception("You cannot debit Product and Teller accounts at the same time"); }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Instafin-Quickbooks Integration", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            


           


            Button b  = (Button)e.OriginalSource;
            ///get the DataContext of the Button's parent/root element
            ///and cast it to a JournalEntry object, which is the type of DataGrid Context
            JournalEntry jE = (JournalEntry)((Button)e.OriginalSource).DataContext;
            MessageBox.Show(jE.ProductName, "Instafin-Quickbooks Integration");

            

                        if(lrdDownload is true)
                        {

                            //create LRD journal entry files 
                            lrdTxn.CreateJournalEntryFile(jE.ProductAccount, jE.TellerAccount, jE.Amount, jE.TransactionType, txndate);
                        }
                        if (usdDownload is true)
                        {
                            //create LRD journal entry files
                            usdTxn.CreateJournalEntryFile(jE.ProductAccount, jE.TellerAccount, jE.Amount, jE.TransactionType, txndate);
                        }

            


            //dataRowView.Row[1]

            //int num = journalentries.Items.Count;

            //MessageBox.Show(user1.Name, "Instafin-Quickbooks Integration");//"Item count"+ num

            // MessageBox.Show("Journal Entry File \nsuccessfuly created", "Instafin-Quickbooks Integration");
        }

        /// <summary>
        /// Use VisualTreeHelper to find the parent object of the specified dependent object 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            try
            {
                List<T> TList = new List<T> { };
                DependencyObject parent = VisualTreeHelper.GetParent(obj);
                if (parent != null && parent is T)
                {
                    TList.Add((T)parent);
                    List<T> parentOfParent = FindVisualParent<T>(parent);
                    if (parentOfParent != null)
                    {
                        TList.AddRange(parentOfParent);
                    }
                }
                else if (parent != null)
                {
                    List<T> parentOfParent = FindVisualParent<T>(parent);
                    if (parentOfParent != null)
                    {
                        TList.AddRange(parentOfParent);
                    }
                }
                return TList;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return null;
            }
        }

        private void qbkProdAccnt_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton qbpa = (RadioButton)e.OriginalSource;
           // bool? debit = 
            prodAccntDebit = qbpa.IsChecked;
            if (prodAccntCredit is true or null)
                prodAccntCredit = false;
        }

        private void qbkProdAccnt1_Click(object sender, RoutedEventArgs e)
        {
            RadioButton qbpa = (RadioButton)e.OriginalSource;
           // bool? credit = 
            prodAccntCredit = qbpa.IsChecked;
            if (prodAccntDebit is true or null)
                prodAccntDebit = false;
        }

        private void qbkTellerAccnt_Click(object sender, RoutedEventArgs e)
        {
            RadioButton qbpa = (RadioButton)e.OriginalSource;
            tellerAccntDebit = qbpa.IsChecked;
            if (tellerAccntCredit is true or null)
                tellerAccntCredit = false;
        }

        private void qbkTellerAccnt1_Click(object sender, RoutedEventArgs e)
        {
            RadioButton qbpa = (RadioButton)e.OriginalSource;

            tellerAccntCredit = qbpa.IsChecked;
            if (tellerAccntDebit is true or null)
                tellerAccntDebit = false;
        }

        private void vaulttransfer_Click(object sender, RoutedEventArgs e)
        {
            //this is the date for which transactions are requested
            DateTime? tDate = txndateCtl.SelectedDate;

            //convert the nullable txnDate to a proper DateTime object
            DateTime tdate = Convert.ToDateTime(tDate);
            string txnDate = getTranxDate(tdate);
            dmdiApp.dmditxn lrdTxn = new dmdiApp.dmditxn();
            dmdiApp.dmditxnusd usdTxn = new dmditxnusd();

            var deposit = from x in MainWindow.JournalEntries
                          where x.TransactionType == "DEPOSIT_DEPOSIT"
                          group x by new
                          {
                              x.Branch
                          };

            var withdrawal = from x in MainWindow.JournalEntries
                             where x.TransactionType == "DEPOSIT_WITHDRAWAL"
                             group x by new
                             {
                                 x.Branch
                             };
            var repayment = from x in MainWindow.JournalEntries
                            where x.TransactionType.Contains("LOAN_REPAYMENT")
                            group x by new
                            { 
                                x.Branch
                            };
            var disbursement = from x in MainWindow.JournalEntries
                               where x.TransactionType.Contains("LOAN_DISBURSEMENT")
                               group x by new
                               {
                                   x.Branch
                               };

            decimal totalDepositM = 0M, totalWithdrawalM = 0M, totalRepaymentM = 0M, totalDisbursementM = 0M;
            decimal totalDepositR = 0M, totalWithdrawalR = 0M, totalRepaymentR = 0M, totalDisbursementR = 0M; // = hqDeposit.Sum(x => x.Amount);
            decimal tellerTotalM = 0M, tellerTotalR = 0M;



            //  decimal totalWithdrawal = hqWithDrawal.Sum(x => x.Amount);
            //  decimal totalRepayment = hqRepayment.Sum(x => x.Amount);

            //LRD calculations for both branches
            if (lrdDownload is true)
            {
                ///
                //MainWindow.JournalEntries
                // lrdTxn.TraansferToVault()
                //repayment
                foreach (var t in repayment)
                {
                    if (t.Key.Branch == "Monrovia")
                        totalRepaymentM = t.Sum(x => x.Amount);

                    if (t.Key.Branch == "Redlight")
                    {
                        totalRepaymentR = t.Sum(x => x.Amount); ;
                    }

                }
                //deposits
                foreach (var t in deposit)
                {
                    if (t.Key.Branch == "Monrovia")
                        totalDepositM = t.Sum(x => x.Amount);

                    if (t.Key.Branch == "Redlight")
                    {
                        totalDepositR = t.Sum(x => x.Amount); ;
                    }

                }
                ///withdrawals
                foreach (var t in withdrawal)
                {
                    if (t.Key.Branch == "Monrovia")
                        totalWithdrawalM = t.Sum(x => x.Amount);

                    if (t.Key.Branch == "Redlight")
                    {
                        totalWithdrawalR = t.Sum(x => x.Amount); ;
                    }

                }
                ///disbursements
                foreach (var t in disbursement)
                {
                    if (t.Key.Branch == "Monrovia")
                        totalDisbursementM = t.Sum(x => x.Amount);

                    if (t.Key.Branch == "Redlight")
                    {
                        totalDisbursementR = t.Sum(x => x.Amount); ;
                    }

                }
                tellerTotalM = totalDepositM + totalRepaymentM - (totalDisbursementM - totalWithdrawalM);
                tellerTotalR = totalDepositR + totalRepaymentR - (totalDisbursementR - totalWithdrawalR);
                lrdTxn.TransferToVault("Teller 2 - Head Office (LRD)", "Cash-in-Vault Head Office (LRD)", tellerTotalM, txnDate);
                lrdTxn.TransferToVault("Teller 2 - Redlight (LRD)", "Cash in Vault - Redlight (LRD)", tellerTotalR, txnDate);
            }
            ///USD calculation for both branches
            if (usdDownload is true)
            {
                //repayment
                foreach (var t in repayment)
                {
                    if (t.Key.Branch == "Monrovia")
                        totalRepaymentM = t.Sum(x => x.Amount);

                    if (t.Key.Branch == "Redlight")
                    {
                        totalRepaymentR = t.Sum(x => x.Amount); ;
                    }

                }
                //deposits
                foreach (var t in deposit)
                {
                    if (t.Key.Branch == "Monrovia")
                        totalDepositM = t.Sum(x => x.Amount);

                    if (t.Key.Branch == "Redlight")
                    {
                        totalDepositR = t.Sum(x => x.Amount); ;
                    }

                }
                ///withdrawals
                foreach (var t in withdrawal)
                {
                    if (t.Key.Branch == "Monrovia")
                        totalWithdrawalM = t.Sum(x => x.Amount);

                    if (t.Key.Branch == "Redlight")
                    {
                        totalWithdrawalR = t.Sum(x => x.Amount); ;
                    }

                }
                ///disbursements
                foreach (var t in disbursement)
                {
                    if (t.Key.Branch == "Monrovia")
                        totalDisbursementM = t.Sum(x => x.Amount);

                    if (t.Key.Branch == "Redlight")
                    {
                        totalDisbursementR = t.Sum(x => x.Amount); ;
                    }

                }
                tellerTotalM = totalDepositM + totalRepaymentM - (totalDisbursementM - totalWithdrawalM);
                tellerTotalR = totalDepositR + totalRepaymentR - (totalDisbursementR - totalWithdrawalR);
                usdTxn.TransferToVault("Teller 1 - Head Office", "Cash-in-Vault - Head Office", totalDepositM, txnDate);
                usdTxn.TransferToVault("Teller 1 - Redlight", "Cash-in-Vault - Redlight", totalDepositR, txnDate);

            }

            MessageBox.Show("Files successfully created ...", "Instafin-Quickbooks Integration", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private static string getTranxDate(DateTime tdate)
        {
            string day = tdate.Day.ToString();
            string month = tdate.Month.ToString();
            string year = tdate.Year.ToString();

            string fullDate = year + "-" + month + "-" + day;
            string txndate = month + "/" + day + "/" + year;
            return txndate;
        }

        private void window_grid_loaded(object sender, RoutedEventArgs e)
        {
            
            WindowState = WindowState.Maximized;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}

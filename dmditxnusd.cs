using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.IO;
using RestSharp;
using WpfApp1;



namespace dmdiApp
{
    public class dmditxnusd
    {

        public  Hashtable loanProducts = new Hashtable(){
                      {"TRACLNEQR","Trade & Comm Loans - USD, Trade & Comm Loans"},
                      {"TRACLN-W","Trade & Comm Loans - USD, Trade & Comm Loans"},
                      {"SERVLNEQR", "Service Loan, Service Loans"},
                      {"SERVLN-W", "Service Loan, Service Loans"},
                      {"PRODLNUSDEQR","Production Loans - USD, Production Loans"},
                      {"PRODNL-W","Production Loans - USD, Production Loans"},
                      {"CONSULNEQR","Consumer Loans - USD, Consumer Loans"},
                      {"AGRICLSWA","Agric Loans, Agric  Loan- Receivable"},
                      {"PHFLN","Private Home Financing, Private Home Finan Loans"},   
                      {"SAVLDLN", "Savings Led, Savings Led"},
                      {"RESTRUCTUREAL",",Restructured Loans"},
                      {"STAFFL","Staff Housing Loan - USD,"},
                      {"ALSCF1",""}                
                   };

        Hashtable depositProdsMap = new Hashtable(){
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
        
        public string getTxnLiveUSD(string txndate){
                var client = new RestClient("https://dmdiusd.instafin.com/submit/instafin.SearchAllTransactions");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic YzcwMjI4MTkxOGQ1NDE2Njg4OTc3YmYzZjUzM2Y1NTg6ZTJjNDc4ODQ3MzUxNGMxYjhjZDNmZjUwMjZlMjdjMDE=");
            request.AddHeader("Content-Type", "text/plain");
            var body = @"{
            " + "\n" +
            @" ""filter"": {
            " + "\n" +
            @" ""startDate"":"+ '"'+txndate+'"' + ","
             + "\n" +
            @" ""endDate"":" + '"'+ txndate+'"' + ","
             + "\n" +
            @" ""branchId"": null,
            " + "\n" +
            @" ""creditOfficerId"": null,
            " + "\n" +
            @" ""centreId"" : null,
            " + "\n" +
            @" ""organisation"":
            " + "\n" +
            @"{
            " + "\n" +
            @" ""organisationId"": null,
            " + "\n" +
            @" ""withoutCreditOfficer"": false,
            " + "\n" +
            @" ""withoutCentre"": false
            " + "\n" +
            @" },
            " + "\n" +
            @" ""transactionTypes"": [],
            " + "\n" +
            @" ""transactionStatuses"": [],
            " + "\n" +
            @" ""paymentMethods"": [],
            " + "\n" +
            @" ""revertedTransactions"": true,
            " + "\n" +
            @" ""successfulTransactions"": true,
            " + "\n" +
            @" ""revertTransactions"": true,
            " + "\n" +
            @" ""fromAmount"": null,
            " + "\n" +
            @" ""toAmount"": null,
            " + "\n" +
            @" ""inAbsoluteAmounts"": false,
            " + "\n" +
            @" ""username"": null,
            " + "\n" +
            @" ""users"": null,
            " + "\n" +
            @" ""forOtherBranchTransactions"": true,
            " + "\n" +
            @" ""fromOtherBranchTransactions"": true,
            " + "\n" +
            @" ""withinOwnBranchTransactions"": true,
            " + "\n" +
            @" ""transactionIDs"": [],
            " + "\n" +
            @" ""clients"": [],
            " + "\n" +
            @" ""accountIDs"": [],
            " + "\n" +
            @" ""products"": [],
            " + "\n" +
            @" ""isCashRelevant"": false
            " + "\n" +
            @" },
            " + "\n" +
            @" ""pagination"":{
            " + "\n" +
            @" ""beforeID"": null,
            " + "\n" +
            @" ""limit"": 1000
            " + "\n" +
            @"}
            " + "\n" +
            @"}";
            request.AddParameter("text/plain", body,  ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            
            return response.Content;
         }

        public void createIIFFile()
        {
           
           /*
            string accntName;
            string txnType = "TRNSTYPE";
            string date = DateTime.Now.ToShortDateString();
            string amnt= "0.0";
            */

            string fileName = @"C:\IIF_Files\USD\je.iif";
            if (!File.Exists(fileName)) 
            {
                // Create the journal entry file headers.
                using (StreamWriter writer = File.CreateText(fileName)) 
                {
                    writer.WriteLine("!TRNS\tTRNSTYPE\tDATE\tACCNT\tAMOUNT");
                    writer.WriteLine("!SPL\tTRNSTYPE\tDATE\tACCNT\tAMOUNT");
                    writer.WriteLine("!ENDTRNS");
                }	

                //create the journal entry transactions
              //  foreach (var item in collection)
                {
                    
                }
            }
        }

        public void createJsonFFileUSD(string json)
        {
           
           

            string fileName = @"C:\IIF_Files\USD\jsonData"+ DateTime.Now.ToString("MM_dd_yyyy") +".txt";
           
                using (StreamWriter writer = File.CreateText(fileName)) 
                {
                    writer.WriteLine(json);
                   
                }	

        }

        
        public void calcLoanProducts_Principle(AllTransactions txns)
        {
            //decimal sumAllrodPrincipals = 0;// this is the sum of all principal payments
            decimal sumAllProdPrincipalsMON = 0;// this is the sum of all principal payments
            decimal sumAllProdPrincipalsRED = 0;// this is the sum of all principal payments

             dmditxnusd txn = new dmditxnusd(); // create a new member of the class to access the hashtable
            var lnrepaymentGp =  from s in txns.transactions
                    where s.productType == "Loan" && s.transactionType == "LOAN_REPAYMENT"
                    group s by new {
                        s.productName, s.transactionType, s.productID,s.paymentMethod, s.transactionBranch
                    };
                    string fileName = @"C:\IIF_Files\USD\loanProdsJE_Principle.iif";
            using(StreamWriter writer = File.CreateText(fileName))
            {

                    foreach (DictionaryEntry prodId in txn.loanProducts)//qbaccntMap
                    {
                        Console.WriteLine("Loan Name {0}", prodId.Key); //.Split(",",2,StringSplitOptions.None)[1]);    
                    }

                    
                    //create the journal entry file header row
                    writer.WriteLine("!TRNS\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!SPL\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!ENDTRNS");
                foreach (var t in lnrepaymentGp)
                    {
                     
                        {
                            string principalAccnt = loanProducts[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[1];
                           // string interestAccnt = qbaccntMap[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[0];

                            
                                                      
                            //Cr product account in QBdate- 12/28/2021
                            writer.WriteLine("TRNS\t"+ DateTime.Now.ToString("MM/dd/yyyy")+ "\tGENERAL JOURNAL\t"+ principalAccnt.TrimStart() +"\t12345678\tClient-2\tClass3\t" + (0M -t.Sum(x=>decimal.Parse(x.principal ?? "0.0")))+"\tPrincipal paid");
                            
                            //check the transaction branch
                            if(t.Key.transactionBranch.Equals("Monrovia"))
                            {
                                //add update the principals
                                sumAllProdPrincipalsMON += t.Sum(x=>decimal.Parse(x.principal ?? "0.0"));


                                    //check the payment method
                                    //to determine the right income account to debit
                                    //note that debit (Dr) amounts are always positive
                                    // and credit(Cr) amounts are always negative
                                   // for the Disbursement transaction type the product accunt is debited
                                   //and the payment method account Cas, Cheque, Mobile Money is credited
                                    
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy") +"\tGENERAL JOURNAL\tTeller 1 - Head Office\t12345678\tClient-2\tClass3\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy") + "GENERAL JOURNAL\tTeller 1 - Head Office\t12345678\tClient-2\tClass3\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t" + DateTime.Now.ToString("MM/dd/yyyy")+ "\tGENERAL JOURNAL\tMobile Money - USD\t12345678\tClient-2\tClass3\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }


                            }
                            if(t.Key.transactionBranch.Equals("Redlight"))
                            {
                                sumAllProdPrincipalsRED += t.Sum(x => decimal.Parse(x.principal ?? "0.0")); 
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy") +"\tGENERAL JOURNAL\tTeller 1 - Redlight\t12345678\tClient-2\tClass3\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy") + "\tGENERAL JOURNAL\tTeller 1 - Redlight\t12345678\tClient-2\tClass3\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy") + "\tGENERAL JOURNAL\tMobile Money USD\t12345678\tClient-2\tClass3\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t");
                                                        writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }
                            }
                        }

                        //calculate the total amount of principal paid for all products
                        //sumAllrodPrincipals += t.Sum(x => decimal.Parse(x.principal ?? "0.0"));
                      } //end of foreach 
                                    
                    //last transction is between the Teller and the  Vault
                    //Monrovia
                    //Create Journal Entry of type transfer
               // writer.WriteLine("TRNS\t"+ DateTime.Now.ToString("MM/dd/yyyy")+ "\tGENERAL JOURNAL\tTeller 2 - Head Office (LRD)\t12345678\tClient-2\tClass3\t" + (0M - sumAllProdPrincipalsMON)+"\tCash sent to vault");
                
            } //end of using statement

            bankAccnt_TransferUSD(sumAllProdPrincipalsMON, sumAllProdPrincipalsRED, @"C:\IIF_Files\USD\bankAccnt_PrincipalTransferUSD.iif");

                    Console.WriteLine("Total Principal paid for all products {0}   ,{1}", sumAllProdPrincipalsMON, sumAllProdPrincipalsRED);

        }

        public void calcDisbursementProducts(AllTransactions txns)
        {
                    var lndisbursementGp =  from s in txns.transactions
                    where  s.productType == "Loan" && s.transactionType == "LOAN_DISBURSEMENT"
                    group s by new {
                        s.productName, s.transactionType, s.productID,s.paymentMethod, s.transactionBranch
                    };
        
            string fileName = @"C:\IIF_Files\USD\loanDisbursementProdsJE.iif";
            using(StreamWriter writer = File.CreateText(fileName))
            {

                  
                    
                    //create the journal entry file header row
                    writer.WriteLine("!TRNS\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!SPL\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!ENDTRNS");
                string principalAccnt = "";
                foreach (var t in lndisbursementGp)
                {

                           try{
                                principalAccnt = loanProducts[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[1];
                           }
                           catch(Exception ex)
                           {
                            continue;
                           }
                            
                           // string interestAccnt = loanProducts[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[0];

                            
                             //Dr product account in QBdate- 12/28/2021
                             //principalAccnt

                             string accnt = principalAccnt.TrimStart();
                            /* Console.WriteLine("Principal Account "+accnt);
                            Console.WriteLine("\nString Lengths \n");
                            Console.WriteLine("Agric Loan : {0}  principalAcnt variable {1}", "Agric Loan".Length, accnt.Length);
                            */
                            writer.WriteLine("TRNS\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t"+accnt+"\t12345678\tClient-S\tClass4\t" + t.Sum(x=>decimal.Parse(x.amount ?? "0.0"))  +"\tDisbursement");//+"\t\t"
                           
                           if(t.Key.transactionBranch.Equals("Monrovia"))
                            {
                                    //check the payment method
                                    //to determine the right income account to debit
                                    //note that debit (Dr) amounts are always positive
                                    // and credit(Cr) amounts are always negative
                                    // for the Disbursement transaction type the prt.Sum(x=>decimal.Parse(x.amount ?? "0.0"))product accunt is debited
                                    //and the payment method account Cash, Cheque, Mobile Money is credited
                                    
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t" + DateTime.Now.ToString("MM/dd/yyyy") +"\tGENERAL JOURNAL"+ "\tTeller 1 - Head Office\t12345678\tClient-S\tClass4\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\tDisbursement");
                                                    writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+DateTime.Now.ToShortDateString()+"\tGENERAL JOURNAL"+ "\tTeller 1 - Head Office\t12345678\tClient-S\tClass4\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0"))) +"\tDisbursement");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToShortDateString()+"\tGENERAL JOURNAL"+"\tMobile Money - USD\t12345678\tClient-S\tClass4\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0"))) +"\tDisbursement");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }


                            }
                            if(t.Key.transactionBranch.Equals("Redlight"))
                            {
                                //these are credit tranctions thus the amount must be negative
                                switch (t.Key.paymentMethod)
                                {
                                     case "Cash" :  writer.WriteLine("SPL\t" + DateTime.Now.ToString("MM/dd/yyyy") +"\tGENERAL JOURNAL"+ "\tTeller 1 - Redlight\t12345678\tClient-S\tClass4\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\tDisbursement");
                                                    writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+DateTime.Now.ToShortDateString()+"\tGENERAL JOURNAL"+ "\tTeller 1 - Redlight\t12345678\tClient-S\tClass4\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\tDisbursement");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToShortDateString()+"\tGENERAL JOURNAL"+"\tMobile Money - USD\t12345678\tClient-S\tClass4\t" +  (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\tDisbursement");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }
                            }
                                      
                }
            }

        }

        public void calcSavingsDepositProducts(AllTransactions txns)
        {
                    var depositGp =  from s in txns.transactions
                    where  s.productType == "Deposit" && s.transactionType == "DEPOSIT_DEPOSIT"
                    group s by new {
                        s.productName, s.transactionType, s.productID,s.paymentMethod, s.transactionBranch
                    };
        
            
            string fileName = @"C:\IIF_Files\USD\SavingsDepositProds.iif";

             decimal sumDepositsHO = 0;
            decimal sumDepositsRL = 0;
            using(StreamWriter writer = File.CreateText(fileName))
            {

                 
                    
                    //create the journal entry file header row
                    //!TRNS	DATE	TRNSTYPE	ACCNT	DOCNUM	NAME	CLASS	AMOUNT	MEMO
                    writer.WriteLine("!TRNS\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!SPL\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!ENDTRNS");
                foreach (var t in depositGp)
                {
                   Console.WriteLine("Deposit products : {0}  Amount Deposited : {1}, ProdID : {2}", t.Key.productName, t.Sum(x=>decimal.Parse(x.amount ?? "0.0")), t.Key.productID); 
                            //string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[1];
                           string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[0];
                            writer.WriteLine("TRNS\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t"+Accnt+ "\t12345678\tGlobetekServices\tClass1\t" + (0M + t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");//
                           
                           if(t.Key.transactionBranch.Equals("Monrovia"))
                            {
                                sumDepositsHO += t.Sum(x=> decimal.Parse( x.amount ?? "0"));
                                    //check the payment method
                                    //to determine the right income account to debit
                                    //note that debit (Dr) amounts are always positive
                                    // and credit(Cr) amounts are always negative
                                    // for the Disbursement transaction type the product accunt is debited
                                    //and the payment method account Cas, Cheque, Mobile Money is credited
                                    
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Head Office\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                    writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Head Office\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Mobile Money - USD\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }


                            }
                            if(t.Key.transactionBranch.Equals("Redlight"))
                            {
                                sumDepositsRL += t.Sum(x=> decimal.Parse( x.amount ?? "0"));
                                //these are credit tranctions thus the amount must be negative
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Redlight\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Redlight\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Mobile Money - USD\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }
                            }
                    //Console.WriteLine(t.Key);
                    // if(t.Key..Contains()
                    // foreach (var _t in t)
                    {
                    // if(txn.getPaymentMethod(_t.transactionType) == "REPAYMENT")
                        //Console.WriteLine("Interest : " + _t.interest + "\t"+ "Principal : "+ _t.principal +"\t"+ "Amount : "+_t.amount);
                        Console.WriteLine("\nAmount :{0}   ProductID {1}" ,t.Sum( n=>decimal.Parse(n.amount ?? "0.0")), t.Key.productID);

                    } 
                    
                }// end of Foreach loop

                bankAccnt_TransferUSD(sumDepositsHO, sumDepositsRL, @"C:\IIF_Files\USD\bankTransfer_SavingsDeposits.iif");
            }

        }

        public void calcLoanProducts_Interests(AllTransactions txns)
        {
            var lnrepaymentGp =  from s in txns.transactions
                    where s.productType == "Loan" && s.transactionType == "LOAN_REPAYMENT"
                    group s by new {
                        s.productName, s.transactionType, s.productID,s.paymentMethod, s.transactionBranch
                    };
                    string fileName = @"C:\IIF_Files\USD\loanProdsJE_Interests.iif";
                     decimal sumAllInterestMO = 0;
                decimal sumAllInterestRL = 0;
            using(StreamWriter writer = File.CreateText(fileName))
            {
                           
                    
                //create the journal entry file header row
                //  writer.WriteLine("!CUST\tNAME\tBADDR1\tBADDR2\tBADDR3\tBADDR4\tBADDR5\tSADDR1\tSADDR2\tSADDR3\tSADDR4\tSADDR5\tPHONE1\tPHONE2\tFAXNUM\tEMAIL\tNOTE\tCONT1\tCONT2\tCTYPE\tTERMS\tTAXABLE\tLIMIT\tRESALENUM\tREP\tTAXITEM\tNOTEPAD\tSALUTATION\tCOMPANYNAME\tFIRSTNAME\tMIDINIT\tLASTNAME");
                //   writer.WriteLine("CUST	Customer	Joe Customer	444 Road Rd	\"Anywhere, AZ 85740\"	USA");
                    writer.WriteLine("!TRNS\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!SPL\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!ENDTRNS");
                //quickbooks account that the product maps to?
                string interestAccnt;
                foreach (var t in lnrepaymentGp)
                    {
                    //   Console.WriteLine(t.Key);

                    //check the product to determine which quick books receivable accnts the product maps to
                    //  if(t.Key.productID.Equals("TRACLNEQR"))
                    try
                    {
                        interestAccnt = loanProducts[t.Key.productID].ToString().Split(",", 2, StringSplitOptions.None)[0];
                    }
                    catch (Exception ex)
                    {

                        continue;
                    }
                            

                            
                            //Cr product Interest account in QBdate- 12/28/2021
                            writer.WriteLine("TRNS\t"+ DateTime.Now.ToString("MM/dd/yyyy") +"\tGENERAL JOURNAL" +"\t"+ interestAccnt.TrimStart() +"\t12345678\tClient-2\tClass2\t" + (0M -t.Sum(x=>decimal.Parse(x.interest ?? "0.0")))+"\tInterest paid");
                            //check the transaction branch
                            if(t.Key.transactionBranch.Equals("Monrovia"))
                            {
                                 sumAllInterestMO += t.Sum(x=>decimal.Parse(x.interest ?? "0.0"));
                                    //check the payment method
                                    //to determine the right income account to debit
                                    //note that debit (Dr) amounts are always positive
                                    // and credit(Cr) amounts are always negative
                                   // for the Disbursement transaction type the product accunt is debited
                                   //and the payment method account Cas, Cheque, Mobile Money is credited
                                    
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  //writer.WriteLine("SPL\t\tGENERAL JOURNAL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tCash-In-Vault - Head Off (LRD)\t\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t\t");
                                                   writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy") +"\tGENERAL JOURNAL\tTeller 1 - Head Office\t12345678\tClient-2\tClass2\t" + t.Sum(x=>decimal.Parse(x.interest ?? "0.0"))+"\t"); 
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": //writer.WriteLine("SPL\t\tGENERAL JOURNAL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tTeller 2 - Head Office (LRD)\t\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t\t");
                                                   writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy") +"\tGENERAL JOURNAL\tTeller 1 - Head Office\t12345678\tClient-2\tClass2\t" + t.Sum(x=>decimal.Parse(x.interest ?? "0.0"))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": //writer.WriteLine("SPL\t\tGENERAL JOURNAL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tMobile Money - LRD\t\t" + t.Sum(x=>decimal.Parse(x.principal ?? "0.0"))+"\t\t");
                                                         writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\tMobile Money - USD\t12345678\tClient-2\tClass2\t" + t.Sum(x=>decimal.Parse(x.interest ?? "0.0"))+"\t");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }


                            }
                            if(t.Key.transactionBranch.Equals("Redlight"))
                            {
                                sumAllInterestRL += t.Sum(x=>decimal.Parse(x.interest ?? "0.0"));
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" : writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy") + "\tGENERAL JOURNAL\tTeller 1 - Redlight\t12345678\tClient-2\tClass2\t" + t.Sum(x=>decimal.Parse(x.interest ?? "0.0"))+"\t"); 
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t" +DateTime.Now.ToString("MM/dd/yyyy") + "\tGENERAL JOURNAL\tTeller 1 - Redlight\t12345678\tClient-2\tClass2\t" + t.Sum(x=>decimal.Parse(x.interest ?? "0.0"))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money":writer.WriteLine("SPL\t" + DateTime.Now.ToString("MM/dd/yyyy") + "\tGENERAL JOURNAL\tMobile Money USD\t12345678\tClient-2\tClass2\t" + t.Sum(x=>decimal.Parse(x.interest ?? "0.0"))+"\t");
                                                        writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }
                            }
                        
                      } //end of using statement
               } //end of using
            bankAccnt_TransferUSD(sumAllInterestRL, sumAllInterestRL, @"C:\IIF_Files\USD\bankAccnt_InterestTransferUSD.iif");


        }

        public void bankAccnt_TransferUSD(decimal amountHO, decimal amountRL, string fileName)
        {
            //string fileName = @"C:\IIF_Files\bankAccnt_TransferLRD.iif";

            using(StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine("!TRNS\tTRNSID\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO\tCLEAR");
                writer.WriteLine("!SPL\tSPLID\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO\tCLEAR");
                writer.WriteLine("!ENDTRNS");
               
               // Head Office  debit teller credit Cash-In-Vault
                writer.WriteLine("TRNS\t\t"+"TRANSFER\t"+DateTime.Now.ToString("MM/dd/yyyy")+"\tTeller 1 - Head Office\t\t" + amountHO +"\t123\tFunds Transfer\tN");
                writer.WriteLine("SPL\t\t" +"TRANSFER\t"+ DateTime.Now.ToString("MM/dd/yyyy") + "\tCash-in-Vault - Head Office\t\t" +(0M - amountHO)  +"\t789\tFunds Transfer\tN"); //
                writer.WriteLine("ENDTRNS");

                //Redlight
                writer.WriteLine("TRNS\t\t"+ "TRANSFER\t" +DateTime.Now.ToString("MM/dd/yyyy")+ "\tTeller 1 - Redlight\t\t" + amountRL +"\t178\tFunds Transfer\tN");
                writer.WriteLine("SPL\t\t" + "TRANSFER\t"+ DateTime.Now.ToString("MM/dd/yyyy") + "\tCash-in-Vault - Redlight\t\t" +(0M - amountRL)  +"\t678\tFunds Transfer\tN");
                writer.WriteLine("ENDTRNS");

            }

        }

        public void calcWithdrawalDepositProducts(AllTransactions txns)
        {
                    var depositGp =  from s in txns.transactions
                    where  s.transactionType.Contains("WITHDRAWAL") //s.productType == "Deposit"
                    group s by new {
                        s.productName, s.transactionType, s.productID,s.paymentMethod, s.transactionBranch
                    };

            decimal sumDepositsHO = 0;
            decimal sumDepositRL = 0;
            string fileName = @"C:\IIF_Files\USD\WithDrawalDepositProds.iif";
            using(StreamWriter writer = File.CreateText(fileName))
            {

                   /* Hashtable depositProdsMap = new Hashtable(){
                      {"SVGCONLN","Collateral Savings - LRD, ''"},
                      {"SVGSERLN","Collateral Savings - LRD, ''"},
                      {"SVGSERLN - W","Collateral Savings - LRD, ''"},
                      {"SVGTRCLN", "Collateral Savings, ''"},
                      {"LRDREG", "Regular Savings - LRD, ''"},
                      {"SSD", "Regular Savings - LRD, ''"},
                      {"SSW","Regular Savings - LRD, ''"},
                      {"SSM","Regular Savings - LRD, ''"},
                      {"REGSAVG","Regular Savings - LRD,''"},
                      {"SVGPPHFLN","Collateral Savings - LRD,''"},
                      {"SVGPPRIHLN","Collateral Savings - LRD, ''"},
                      {"SVGPRDLN","Collateral Savings - LRD,''"},
                      {"SVGPRODLN - W","Collateral Savings - LRD,''"},
                      {"SVGTRDCOM","Collateral Savings - LRD,''"},
                      {"CSA","CLIENTS SAVINGS - LRD,''"},
                      {"SVGSTAFF","Regular Savings - LRD,''"},
                      {"SVGLED","Savings Led - LRD,''"},
                      {"TERMDEP","Term Deposits - LRD,''"}                       
                   }; */

                  // foreach (DictionaryEntry prodId in depositProdsMap)
                  //  {
                   //     Console.WriteLine("Deposit Prod Name {0}", prodId.Key); //.Split(",",2,StringSplitOptions.None)[1]);    
                  //  }

                    
                    //create the journal entry file header row
                    //!TRNS	DATE	TRNSTYPE	ACCNT	DOCNUM	NAME	CLASS	AMOUNT	MEMO
                    writer.WriteLine("!TRNS\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!SPL\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!ENDTRNS");
                foreach (var t in depositGp)
                {
                   Console.WriteLine("Deposit products : {0}  Amount Deposited : {1}, ProdID : {2}", t.Key.productName, t.Sum(x=>decimal.Parse(x.amount ?? "0.0")), t.Key.productID); 
                            //string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[1];
                           string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[0];
                    
                            //Console.WriteLine("PrincipalAcnt : {0} InterestAcnt : {1}", principalAccnt,interestAccnt);
                             //Dr product account in QBdate- 12/28/2021
                             //principalAccnt

                           //  string accnt = Accnt.TrimStart();
                          //  Console.WriteLine("Principal Account "+accnt);
                           // Console.WriteLine("\nString Lengths \n");
                           // Console.WriteLine("Agric Loan : {0}  principalAcnt variable {1}", "Agric Loan".Length, accnt.Length);

                            writer.WriteLine("TRNS\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t"+Accnt+ "\t12345678\tGlobetekServices\tClass1\t" + (0M + t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");//
                           
                           if(t.Key.transactionBranch.Equals("Monrovia"))
                            {

                                    sumDepositsHO += t.Sum(x=> decimal.Parse(x.amount ?? "0"));
                                    //check the payment method
                                    //to determine the right income account to debit
                                    //note that debit (Dr) amounts are always positive
                                    // and credit(Cr) amounts are always negative
                                    // for the Disbursement transaction type the product accunt is debited
                                    //and the payment method account Cas, Cheque, Mobile Money is credited
                                    
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Head Office\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                    writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Head Office\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.interest ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Mobile Money - USD\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.interest ?? "0.0")))+"\t");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }


                            }
                            if(t.Key.transactionBranch.Equals("Redlight"))
                            {
                                sumDepositRL += t.Sum(x=> decimal.Parse(x.amount ?? "0"));
                                //these are credit tranctions thus the amount must be negative
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Redlight\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Redlight\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Mobile Money - USD\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }
                            }
                    Console.WriteLine(t.Key);
                    // if(t.Key..Contains()
                    // foreach (var _t in t)
                    {
                    // if(txn.getPaymentMethod(_t.transactionType) == "REPAYMENT")
                        //Console.WriteLine("Interest : " + _t.interest + "\t"+ "Principal : "+ _t.principal +"\t"+ "Amount : "+_t.amount);
                        Console.WriteLine("\nAmount :{0}   ProductID {1}" ,t.Sum( n=>decimal.Parse(n.amount ?? "0.0")), t.Key.productID);

                    } 
                    
                }//end foreach loop
                bankAccnt_TransferUSD(sumDepositsHO, sumDepositRL, @"C:\IIF_Files\USD\bankTransfer_Witdrawal.iif");

            }

        }

        public void calcDepositDepositsProducts(AllTransactions txns)
        {
             var depositGp =  from s in txns.transactions
                    where  s.transactionType.Contains("WITHDRAWAL") //s.productType == "Deposit"
                    group s by new {
                        s.productName, s.transactionType, s.productID,s.paymentMethod, s.transactionBranch
                    };

            decimal sumDepositsHO = 0;
            decimal sumDepositsRL = 0;
            string fileName = @"C:\IIF_Files\USD\WithDrawalDepositProds.iif";
            using(StreamWriter writer = File.CreateText(fileName))
            {

                   /* Hashtable depositProdsMap = new Hashtable(){
                      {"SVGCONLN","Collateral Savings - LRD, ''"},
                      {"SVGSERLN","Collateral Savings - LRD, ''"},
                      {"SVGSERLN - W","Collateral Savings - LRD, ''"},
                      {"SVGTRCLN", "Collateral Savings, ''"},
                      {"LRDREG", "Regular Savings - LRD, ''"},
                      {"SSD", "Regular Savings - LRD, ''"},
                      {"SSW","Regular Savings - LRD, ''"},
                      {"SSM","Regular Savings - LRD, ''"},
                      {"REGSAVG","Regular Savings - LRD,''"},
                      {"SVGPPHFLN","Collateral Savings - LRD,''"},
                      {"SVGPPRIHLN","Collateral Savings - LRD, ''"},
                      {"SVGPRDLN","Collateral Savings - LRD,''"},
                      {"SVGPRODLN - W","Collateral Savings - LRD,''"},
                      {"SVGTRDCOM","Collateral Savings - LRD,''"},
                      {"CSA","CLIENTS SAVINGS - LRD,''"},
                      {"SVGSTAFF","Regular Savings - LRD,''"},
                      {"SVGLED","Savings Led - LRD,''"},
                      {"TERMDEP","Term Deposits - LRD,''"}                       
                   }; */

                  // foreach (DictionaryEntry prodId in depositProdsMap)
                  //  {
                   //     Console.WriteLine("Deposit Prod Name {0}", prodId.Key); //.Split(",",2,StringSplitOptions.None)[1]);    
                  //  }

                    
                    //create the journal entry file header row
                    //!TRNS	DATE	TRNSTYPE	ACCNT	DOCNUM	NAME	CLASS	AMOUNT	MEMO
                    writer.WriteLine("!TRNS\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!SPL\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                    writer.WriteLine("!ENDTRNS");
                foreach (var t in depositGp)
                {
                   Console.WriteLine("Deposit products : {0}  Amount Deposited : {1}, ProdID : {2}", t.Key.productName, t.Sum(x=>decimal.Parse(x.amount ?? "0.0")), t.Key.productID); 
                            //string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[1];
                           string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[0];
                    
                            //Console.WriteLine("PrincipalAcnt : {0} InterestAcnt : {1}", principalAccnt,interestAccnt);
                             //Dr product account in QBdate- 12/28/2021
                             //principalAccnt

                           //  string accnt = Accnt.TrimStart();
                          //  Console.WriteLine("Principal Account "+accnt);
                           // Console.WriteLine("\nString Lengths \n");
                           // Console.WriteLine("Agric Loan : {0}  principalAcnt variable {1}", "Agric Loan".Length, accnt.Length);

                            writer.WriteLine("TRNS\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t"+Accnt+ "\t12345678\tGlobetekServices\tClass1\t" + (0M + t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");//
                           
                           if(t.Key.transactionBranch.Equals("Monrovia"))
                            {

                                  sumDepositsHO += t.Sum(x=> decimal.Parse( x.amount ?? "0"));
                                    //check the payment method
                                    //to determine the right income account to debit
                                    //note that debit (Dr) amounts are always positive
                                    // and credit(Cr) amounts are always negative
                                    // for the Disbursement transaction type the product accunt is debited
                                    //and the payment method account Cas, Cheque, Mobile Money is credited
                                    
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Head Office\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                    writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Head Office\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.interest ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Mobile Money - USD\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.interest ?? "0.0")))+"\t");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }


                            }
                            if(t.Key.transactionBranch.Equals("Redlight"))
                            {
                                sumDepositsRL = t.Sum(x=> decimal.Parse(x.amount ?? "0"));
                                //these are credit tranctions thus the amount must be negative
                                switch (t.Key.paymentMethod)
                                {
                                    case "Cash" :  writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Redlight\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Cheque": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Teller 1 - Redlight\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                   writer.WriteLine("ENDTRNS");
                                                   break;
                                    
                                    case "Mobile Money": writer.WriteLine("SPL\t"+ DateTime.Now.ToString("MM/dd/yyyy")+"\tGENERAL JOURNAL\t" + "Mobile Money - USD\t12345678\tGlobetekServices\tClass1\t" + (0M - t.Sum(x=>decimal.Parse(x.amount ?? "0.0")))+"\t");
                                                         writer.WriteLine("ENDTRNS");
                                                        break;
                                    
                                    default: break;
                                }
                            }
                    Console.WriteLine(t.Key);
                    // if(t.Key..Contains()
                    // foreach (var _t in t)
                    {
                    // if(txn.getPaymentMethod(_t.transactionType) == "REPAYMENT")
                        //Console.WriteLine("Interest : " + _t.interest + "\t"+ "Principal : "+ _t.principal +"\t"+ "Amount : "+_t.amount);
                        Console.WriteLine("\nAmount :{0}   ProductID {1}" ,t.Sum( n=>decimal.Parse(n.amount ?? "0.0")), t.Key.productID);

                    } 
                    
                } //forecah loop ends here

                bankAccnt_TransferUSD(sumDepositsHO, sumDepositsRL, @"C:\IIF_Files\USD\bankTransfer_Deopsits.iif");
            }
        }      
       
        public string getJsonDataUSD()
            {
                // Create the journal entry file headers.
                string fileName = @"C:\IIF_Files\USD\jsonData08_19_2022.txt";
                    using(StreamReader reader = File.OpenText(fileName))
                    {
                        return reader.ReadLine();
                    }
            }


        public void LoanProducts_PrincipleList(AllTransactions txns)
        {


            dmditxn txn = new dmditxn(); // create a new member of the class to access the hashtable
            var lnrepaymentGp = from s in txns.transactions
                                where s.productType == "Loan" && s.transactionType == "LOAN_REPAYMENT"
                                group s by new
                                {
                                    s.productName,
                                    s.transactionType,
                                    s.productID,
                                    s.paymentMethod,
                                    s.transactionBranch
                                };


            Hashtable qbaccntMap = new Hashtable(){
                      {"TRACLNEQR","Trade & Comm Loans - LRD, Trade & Comm Loan - LRD"},
                      {"TRACLN-W","Trade & Comm Loans - LRD, Trade & Comm Loan - LRD"},
                      {"SERVLNEQR", "Service Loan - LRD, Service Loans - LRD"},
                      {"SERVLN-W", "Service Loan - LRD, Service Loans - LRD"},
                      {"PRODLNUSDEQR","Production Loan - LRD, Production Loans - LRD"},
                      {"CONSULNEQR","Consumer Loan - LRD, Consumer Loans - LRD"},
                      {"AGRICLSWA","Agric Loan"}
                   };

            foreach (var t in lnrepaymentGp)
            {
                string principalAccnt = null;
                JournalEntry je = new JournalEntry();
                try { 
                    principalAccnt = loanProducts[t.Key.productID].ToString().Split(",", 2, StringSplitOptions.None)[1];
                }
                catch(Exception ex)
                {
                    continue;
                }
                je.ProductName = t.Key.productID;
                je.ProductAccount = principalAccnt;
                je.TransactionType = t.Key.transactionType + "_Principal";

                if (t.Key.transactionBranch.Equals("Monrovia"))
                {



                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.Branch = t.Key.transactionBranch;
                            je.Amount = t.Sum(x => decimal.Parse(x.principal ?? "0.0"));
                            je.TellerAccount = "Teller 1 - Head Office";
                            break;

                        case "Cheque":
                            je.Branch = t.Key.transactionBranch;
                            je.Amount = t.Sum(x => decimal.Parse(x.principal ?? "0.0"));
                            je.TellerAccount = "Teller 1 - Head Office";

                            break;

                        case "Mobile Money":
                            je.Branch = t.Key.transactionBranch;
                            je.Amount = t.Sum(x => decimal.Parse(x.principal ?? "0.0"));
                            je.TellerAccount = "Mobile Money - USD";
                            break;

                        default: break;
                    }


                }
                if (t.Key.transactionBranch.Equals("Redlight"))
                {


                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.Amount = t.Sum(x => decimal.Parse(x.principal ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Redlight";
                            break;

                        case "Cheque":
                            je.Amount = t.Sum(x => decimal.Parse(x.principal ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Redlight";
                            break;

                        case "Mobile Money":
                            je.Amount = t.Sum(x => decimal.Parse(x.principal ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Mobile Money - USD";
                            break;

                        default: break;
                    }
                }



                MainWindow.JournalEntries.Add(je);
            } //end of foreach 


        }

        public void LoanProducts_InterestList(AllTransactions txns)
        {
            var lnrepaymentGp = from s in txns.transactions
                                where s.productType == "Loan" && s.transactionType == "LOAN_REPAYMENT"
                                group s by new
                                {
                                    s.productName,
                                    s.transactionType,
                                    s.productID,
                                    s.paymentMethod,
                                    s.transactionBranch
                                };

            Hashtable qbaccntMap = new Hashtable(){
                      {"TRACLNEQR","Trade & Comm Loans - LRD, Trade & Comm Loan - LRD"},
                      {"TRACLN-W","Trade & Comm Loans - LRD, Trade & Comm Loan - LRD"},
                      {"SERVLNEQR", "Service Loan - LRD, Service Loans - LRD"},
                      {"SERVLN-W", "Service Loan - LRD, Service Loans - LRD"},
                      {"PRODLNUSDEQR","Production Loan - LRD, Production Loans - LRD"},
                      {"CONSULNEQR","Consumer Loan - LRD, Consumer Loans - LRD"},
                      {"AGRICLSWA","Agric Loan"}
                   };

            foreach (var t in lnrepaymentGp)
            {

                JournalEntry je = new JournalEntry();
                string principalAccnt = null;
                string interestAccnt = null;

                try { 
                     principalAccnt = loanProducts[t.Key.productID].ToString().Split(",", 2, StringSplitOptions.None)[1];
                     interestAccnt = loanProducts[t.Key.productID].ToString().Split(",", 2, StringSplitOptions.None)[0];
                }
                catch(Exception ex)
                {
                    continue;
                }
                je.ProductAccount = interestAccnt;
                je.ProductName = t.Key.productID;
                je.TransactionType = t.Key.transactionType + "_Interest";


                if (t.Key.transactionBranch.Equals("Monrovia"))
                {




                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.TellerAccount = "Teller 1 - Head Office";
                            je.Amount = t.Sum(x => decimal.Parse(x.interest ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            break;

                        case "Cheque":
                            je.TellerAccount = "Teller 1 - Head Office";
                            je.Amount = t.Sum(x => decimal.Parse(x.interest ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            break;

                        case "Mobile Money":
                            je.TellerAccount = "Mobile Money - USD";
                            je.Amount = t.Sum(x => decimal.Parse(x.interest ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            break;

                        default: break;
                    }


                }
                if (t.Key.transactionBranch.Equals("Redlight"))
                {

                    je.Branch = "RedLight";
                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.TellerAccount = "Teller 1 - Redlight";
                            je.Amount = t.Sum(x => decimal.Parse(x.interest ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            break;

                        case "Cheque":
                            je.TellerAccount = "Teller 1 - Redlight";
                            je.Amount = t.Sum(x => decimal.Parse(x.interest ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            break;

                        case "Mobile Money":
                            je.TellerAccount = "Mobile Money - USD";

                            break;

                        default: break;
                    }
                }
                MainWindow.JournalEntries.Add(je);
            }

        }


        public void DisbursementProductsList(AllTransactions txns)
        {
            var lndisbursementGp = from s in txns.transactions
                                   where s.productType == "Loan" && s.transactionType == "LOAN_DISBURSEMENT"
                                   group s by new
                                   {
                                       s.productName,
                                       s.transactionType,
                                       s.productID,
                                       s.paymentMethod,
                                       s.transactionBranch
                                   };

            Hashtable qbaccntMap = new Hashtable(){
                      {"TRACLNEQR","Trade & Comm Loans - LRD, Trade & Comm Loan - LRD"},
                      {"TRACLN-W","Trade & Comm Loans - LRD, Trade & Comm Loan - LRD"},
                      {"SERVLNEQR", "Service Loan - LRD, Service Loans - LRD"},
                      {"SERVLN-W", "Service Loan - LRD, Service Loans - LRD"},
                      {"PRODLNUSDEQR","Production Loan - LRD, Production Loans - LRD"},
                      {"CONSULNEQR","Consumer Loan - LRD, Consumer Loans - LRD"},
                      {"AGRICLSWA","'', Agric Loan - LRD"}
                   };




            foreach (var t in lndisbursementGp)
            {
                JournalEntry je = new JournalEntry();
                string principalAccnt = loanProducts[t.Key.productID].ToString().Split(",", 2, StringSplitOptions.None)[1];
                je.ProductAccount = principalAccnt;
                je.ProductName = t.Key.productID;
                je.TransactionType = t.Key.transactionType;


                if (t.Key.transactionBranch.Equals("Monrovia"))
                {
                    //check the payment method
                    //to determine the right income account to debit
                    //note that debit (Dr) amounts are always positive
                    // and credit(Cr) amounts are always negative
                    // for the Disbursement transaction type the prt.Sum(x=>decimal.Parse(x.amount ?? "0.0"))product accunt is debited
                    //and the payment method account Cash, Cheque, Mobile Money is credited

                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            //writer.WriteLine("SPL\t" + DateTime.Now.ToString("MM/dd/yyyy") + "\tGENERAL JOURNAL" + "\tTeller 2 - Head Office (LRD)\t12345678\tClient-S\tClass4\t" + (0M - t.Sum(x => decimal.Parse(x.amount ?? "0.0"))) + "\tDisbursement");
                            // writer.WriteLine("ENDTRNS");
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Head Office";
                            break;

                        case "Cheque":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Head Office";
                            break;

                        case "Mobile Money":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Mobile Money - USD";
                            break;
                        default: break;
                    }


                }
                if (t.Key.transactionBranch.Equals("Redlight"))
                {
                    //these are credit tranctions thus the amount must be negative
                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Redlight";
                            break;

                        case "Cheque":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Redlight";
                            break;

                        case "Mobile Money":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Mobile Money - USD";
                            break;

                        default: break;
                    }
                }

                MainWindow.JournalEntries.Add(je);
            }


        }

        public void SavingsDepositProductsList(AllTransactions txns)
        {
            var depositGp = from s in txns.transactions
                            where s.productType == "Deposit" && s.transactionType == "DEPOSIT_DEPOSIT"
                            group s by new
                            {
                                s.productName,
                                s.transactionType,
                                s.productID,
                                s.paymentMethod,
                                s.transactionBranch
                            };





            foreach (var t in depositGp)
            {
                Console.WriteLine("Deposit products : {0}  Amount Deposited : {1}, ProdID : {2}", t.Key.productName, t.Sum(x => decimal.Parse(x.amount ?? "0.0")), t.Key.productID);
                //string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",",2,StringSplitOptions.None)[1];

                JournalEntry je = new JournalEntry();
                string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",", 2, StringSplitOptions.None)[0];
                je.ProductAccount = Accnt;
                je.ProductName = t.Key.productID;
                je.TransactionType = t.Key.transactionType;



                if (t.Key.transactionBranch.Equals("Monrovia"))
                {

                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Head Office";
                            break;

                        case "Cheque":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Head Office";
                            break;

                        case "Mobile Money":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Mobile Money - USD";
                            break;

                        default: break;
                    }


                }
                if (t.Key.transactionBranch.Equals("Redlight"))
                {

                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Redlight";
                            break;

                        case "Cheque":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Redlight";
                            break;

                        case "Mobile Money":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0.0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Mobile Money - USD";
                            break;

                        default: break;
                    }
                }

                MainWindow.JournalEntries.Add(je);
            }// end of Foreach loop

        }

        public void WithdrawalDepositProductsList(AllTransactions txns)
        {
            var depositGp = from s in txns.transactions
                            where s.transactionType.Contains("WITHDRAWAL") //s.productType == "Deposit"
                            group s by new
                            {
                                s.productName,
                                s.transactionType,
                                s.productID,
                                s.paymentMethod,
                                s.transactionBranch
                            };

            foreach (var t in depositGp)
            {


                JournalEntry je = new JournalEntry();
                string Accnt = depositProdsMap[t.Key.productID].ToString().Split(",", 2, StringSplitOptions.None)[0];

                je.ProductAccount = Accnt;
                je.ProductName = t.Key.productID;
                je.TransactionType = t.Key.transactionType;


                if (t.Key.transactionBranch.Equals("Monrovia"))
                {

                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Head Office";
                            break;

                        case "Cheque":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Head Office";
                            break;

                        case "Mobile Money":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Mobile Money - USD";
                            break;

                        default: break;
                    }


                }
                if (t.Key.transactionBranch.Equals("Redlight"))
                {

                    switch (t.Key.paymentMethod)
                    {
                        case "Cash":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Redlight";
                            break;

                        case "Cheque":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Teller 1 - Redlight";
                            break;

                        case "Mobile Money":
                            je.Amount = t.Sum(x => decimal.Parse(x.amount ?? "0"));
                            je.Branch = t.Key.transactionBranch;
                            je.TellerAccount = "Mobile Money - USD";
                            break;

                        default: break;
                    }
                }
                MainWindow.JournalEntries.Add(je);
            }//end foreach loop

        }

        public void CreateJournalEntryFile(string productAccnt, string tellerAccnt, decimal amount, string transactionTrype, string txnDate)
        {


            string date = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            string fileName = @"C:\IIF_Files\USD\" + "_" + transactionTrype + (new Random()).Next() + date + ".iif";
            using (StreamWriter writer = File.CreateText(fileName))
            {
                //MainWindow mw = new MainWindow();



                //create the journal entry file header row
                //!TRNS	DATE	TRNSTYPE	ACCNT	DOCNUM	NAME	CLASS	AMOUNT	MEMO
                writer.WriteLine("!TRNS\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                writer.WriteLine("!SPL\tDATE\tTRNSTYPE\tACCNT\tDOCNUN\tNAME\tCLASS\tAMOUNT\tMEMO");
                writer.WriteLine("!ENDTRNS");

                //create actual journal entries
                if (MainWindow.prodAccntCredit is true)
                {
                    writer.WriteLine("TRNS\t" + txnDate + "\tGENERAL JOURNAL\t" + productAccnt + "\t12345678\tGlobetekServices\tClass1\t" + (0M - amount) + "\t");//
                }
                if (MainWindow.prodAccntDebit is true)
                {
                    writer.WriteLine("TRNS\t" + txnDate + "\tGENERAL JOURNAL\t" + productAccnt + "\t12345678\tGlobetekServices\tClass1\t" + amount + "\t");//
                }
                if (MainWindow.tellerAccntCredit is true)
                {
                    writer.WriteLine("SPL\t" + txnDate + "\tGENERAL JOURNAL\t" + tellerAccnt + "\t12345678\tGlobetekServices\tClass1\t" + (0M - amount) + "\t");
                }
                if (MainWindow.tellerAccntDebit is true)
                {
                    writer.WriteLine("SPL\t" + txnDate + "\tGENERAL JOURNAL\t" + tellerAccnt + "\t12345678\tGlobetekServices\tClass1\t" + amount + "\t");
                }


                writer.WriteLine("ENDTRNS");


            }

        }

        public void TransferToVault(string tellerAccnt, string vaultAccount, decimal amount, string txnDate)
        {
            string date = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            string fileName = @"C:\IIF_Files\USD\VaultTransfer\vault_TransferUSD" + (new Random()).Next() + date +".iif";

            using (StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine("!TRNS\tTRNSID\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO\tCLEAR");
                writer.WriteLine("!SPL\tSPLID\tTRNSTYPE\tDATE\tACCNT\tNAME\tAMOUNT\tDOCNUM\tMEMO\tCLEAR");
                writer.WriteLine("!ENDTRNS");

                // Head Officedebit teller credit Cash-In-Vault
                writer.WriteLine("TRNS\t\t" + "TRANSFER\t" + txnDate + "\t" + tellerAccnt + "\t\t" + amount + "\t123\tFunds Transfer\tN");
                writer.WriteLine("SPL\t\t" + "TRANSFER\t" + txnDate + "\t"+ vaultAccount+ "\t\t" + (0M - amount) + "\t789\tFunds Transfer\tN"); //
                writer.WriteLine("ENDTRNS");

                //Redlight
                //writer.WriteLine("TRNS\t\t" + "TRANSFER\t" + DateTime.Now.ToString("MM/dd/yyyy") + "\tTeller 2 - Redlight (LRD)\t\t" + amountRL + "\t178\tFunds Transfer\tN");
                // writer.WriteLine("SPL\t\t" + "TRANSFER\t" + DateTime.Now.ToString("MM/dd/yyyy") + "\tCash in Vault - Redlight (LRD)\t\t" + (0M - amountRL) + "\t678\tFunds Transfer\tN");
                //writer.WriteLine("ENDTRNS");

            }
        }

    }
}
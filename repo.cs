using System;
using System.Collections.Generic;
using System.IO;

namespace dmdiApp
{
    public class AllTransactions
    {
       // public  Filter filter;// {get; set;}
       // public  Pagination pagination;//{get; set;}
       public List<Transaction> transactions {get; set;}

    }

    public class Filter{

    public DateTime startDate { get; set; }
    public DateTime endDate {get; set;}
    public string branchId {get; set;}
    public string creditOfficerId{get; set;}
    public string centreId{get; set;}

    public Organisation organisation{get; set;}
    }

    public class Organisation{

        public string organisationId{get; set;}
        public bool withoutCreditOfficer{get; set;}
        public bool withoutCentre{get; set;}
    }

        public class Pagination{
             public string beforeID{get; set;}
             public int limit{get; set;}
        }

        public class Transaction{
        public int id{get; set;}
         public int accountId{get; set;}
         public string accountType{get; set;}
         public string accountExternalId{get;set;}
         public string productType{get;set;}
         public int clientId{get;set;}
         public string clientType{get;set;}
         public string clientName{get;set;}
         public string clientExternalId{get;set;}
         public string clientBranch{get;set;}
         public string clientCreditOfficer{get;set;}
          public string clientCentre{get;set;}
         public string userName{get;set;}
         public string userDisplayName{get;set;}
         public DateTime occurredOn {get;set;}
         public DateTime createdOn {get;set;}
         public string amount{get;set;}
         public string interest {get;set;}
         public string principal{get;set;}
         public string penalty {get;set;}
         public string fee {get;set;}

         public AutomatedFees automatedFees{get; set;}
         public string notes {get; set;}
         public string revertedBy {get; set;}
         public string chequeNumber {get; set;}
         public string chequeBank {get; set;}
         public string paymentMethod {get; set;}
         public string referenceNumber {get; set;}
         public string transactionBranch {get; set;}
         public string transactionType {get; set;}
         public string transactionStatus {get; set;}
         public string servicesProductID {get; set;}
         public string revertCommandID {get; set;}
         public string isRevertable {get; set;}
         public string stateDescription {get; set;}

         public string resolvedOn {get; set;}
         public string resolvedBy {get; set;}
         
         public string resolvedNotes {get; set;}
         
         public string category {get; set;}
         public string earlyTerminationClosingMethod {get; set;}
         //public string[] products{get; set;}
         public string productID {get; set;}
         public string productName{get; set;}
        }

        public class AutomatedFees{

        }
}
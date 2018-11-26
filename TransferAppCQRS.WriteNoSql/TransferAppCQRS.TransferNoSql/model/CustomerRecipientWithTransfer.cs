using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TransferAppCQRS.repository;

namespace TransferAppCQRS.TransferNoSql.model
{
    public class CustomerRecipientWithTransfer : CustomerWithTransfer
    {
        public CustomerRecipientWithTransfer(Transfer transfer, IConfiguration _config) : base(_config, transfer)
        {
            SetAccount();
            
            DoneTransfers = new List<TransferSummary>();        
            ReceivedTransfers = new List<TransferSummary>();   
        }
        public override void SetAccount()
        {
            this.Account = new AccountSummary(_transfer.Recipient);
            var account = new MongoDbService<Account>(_config, "accountCollection").GetById(_transfer.RecipientGuid);            
            var customer = account.Customer;
            this.Id = customer.Id;
            this.Name = customer.Name;   
        }
        public override void HandleCustomer()
        {
            var recipientWithTransfer = new MongoDbService<CustomerWithTransfer>(_config, "customerWithTransferCollection")
                .GetById(Id);

            var originAccount = new MongoDbService<Account>(_config, "accountCollection")
                .GetById(_transfer.OriginGuid);

             var transferSummary = new TransferSummary()
            {
                Id = _transfer.Id,
                CustomerGuid = originAccount.Customer.Id,
                CustomerName = originAccount.Customer.Name,
                CustomerAccountGuid = _transfer.Origin.Id,
                CustomerAccountAgency = _transfer.Origin.Agency,
                CustomerAccountNumber = _transfer.Origin.Number,
                Description = _transfer.Description,
                ScheduledDate = _transfer.ScheduledDate
            };

            if (recipientWithTransfer != null)
            {                
                //DoneTransfers = recipientWithTransfer.DoneTransfers;
                //ReceivedTransfers = recipientWithTransfer.ReceivedTransfers;
                ReceivedTransfers.Add(transferSummary);
                CurrentBalance = CurrentBalance + _transfer.Value;

                new MongoDbCustomerTransferService(_config, "customerWithTransferCollection")
                    .UpsertReceivedTransfer(transferSummary, Id);
            }
            else
            {
                CurrentBalance = _transfer.Value;
                ReceivedTransfers.Add(transferSummary);
                new MongoDbService<CustomerWithTransfer>(_config, "customerWithTransferCollection")
                    .InsertOne(this);
            }

            
        }
    }
}
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TransferAppCQRS.repository;

namespace TransferAppCQRS.TransferNoSql.model
{
    public class CustomerOriginWithTransfer : CustomerWithTransfer
    {
        public CustomerOriginWithTransfer(Transfer transfer, IConfiguration _config) : base(_config, transfer)
        {
            SetAccount();

            DoneTransfers = new List<TransferSummary>();
            ReceivedTransfers = new List<TransferSummary>();
        }
        public override void SetAccount()
        {
            this.Account = new AccountSummary(_transfer.Origin);
            var account = new MongoDbService<Account>(_config, "accountCollection").GetById(_transfer.OriginGuid);            
            var customer = account.Customer;
            this.Id = customer.Id;
            this.Name = customer.Name;            
        }
        public override void HandleCustomer()
        {
            var customerWithTransfer = new MongoDbService<CustomerWithTransfer>(_config, "customerWithTransferCollection")
                .GetById(Id);

            var recipientAccount = new MongoDbService<Account>(_config, "accountCollection")
                .GetById(_transfer.RecipientGuid);

            var transferSummary = new TransferSummary()
            {
                Id = _transfer.Id,
                CustomerGuid = recipientAccount.Customer.Id,
                CustomerName = recipientAccount.Customer.Name,
                CustomerAccountGuid = recipientAccount.Id,
                CustomerAccountAgency = recipientAccount.Agency,
                CustomerAccountNumber = recipientAccount.Number,
                Description = _transfer.Description,
                ScheduledDate = _transfer.ScheduledDate
            };

            if (customerWithTransfer != null)
            {
                
                DoneTransfers.Add(transferSummary);
                CurrentBalance = CurrentBalance - _transfer.Value;
                // ReceivedTransfers = customerWithTransfer.ReceivedTransfers;

                new MongoDbCustomerTransferService(_config, "customerWithTransferCollection")
                    .UpsertDoneTransfer(transferSummary, Id);

            }
            else
            {

                CurrentBalance = _transfer.Value * -1;
                DoneTransfers.Add(transferSummary);
                new MongoDbService<CustomerWithTransfer>(_config, "customerWithTransferCollection")
                    .InsertOne(this);
            }

            
        }
    }
}
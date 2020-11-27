using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCTraining.Models;
using MVCTraining.Repositories;
using MVCTraining.ViewModels;

namespace MVCTraining.Service
{
    public class TrackSpendingService
    {
        private readonly IRepository<AccountBook> _accountBookRep;
        private readonly IUnitOfWork _unitOfWork;

        public TrackSpendingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountBookRep = new Repository<AccountBook>(unitOfWork);
        }

        //因viewModel設計導致無法使用IEnumerable<>
        public TrackSpendingViewModel GetAll()
        {
            var source = _accountBookRep.GetAll().OrderByDescending(x => x.Dateee);
            var list = source.Select(accountBook => new TrackSpending()
            {
                Category = accountBook.Categoryyy,
                Money = accountBook.Amounttt,
                Date = accountBook.Dateee,
                Description = accountBook.Remarkkk

            }).ToList();
            var resault = new TrackSpendingViewModel()
            {
                List = list
            };
            return resault;
        }

        public void Add(AccountBook accountBook)
        {
            accountBook.Id = Guid.NewGuid();
            _accountBookRep.Create(accountBook);
        }

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}
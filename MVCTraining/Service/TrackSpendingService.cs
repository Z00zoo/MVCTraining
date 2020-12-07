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
        //改用TrackSpendingViewModel2
        public IEnumerable<TrackSpendingViewModel2> GetAll()
        {
            var source = _accountBookRep.GetAll().OrderByDescending(x => x.Dateee);
            var resault = source.Select(accountBook => new  TrackSpendingViewModel2()
            {
                Categoryyy = accountBook.Categoryyy,
                Amounttt = accountBook.Amounttt,
                Dateee = accountBook.Dateee,
                Remarkkk = accountBook.Remarkkk

            }).ToList();
            return resault;
        }

        public void Add(AccountBook accountBook)
        {
            accountBook.Id = Guid.NewGuid();
            _accountBookRep.Create(accountBook);
        }

        public void Edit(AccountBook accountBook)
        {
            _accountBookRep.Update(accountBook);
        }

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}
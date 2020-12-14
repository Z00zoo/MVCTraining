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
            var source = _accountBookRep.GetAll().Take(20).OrderByDescending(x => x.Dateee);
            var resault = source.Select(accountBook => new  TrackSpendingViewModel2
            {
                Id = accountBook.Id,
                Categoryyy = accountBook.Categoryyy,
                Amounttt = accountBook.Amounttt,
                Dateee = accountBook.Dateee,
                Remarkkk = accountBook.Remarkkk,
            }).ToList();
            return resault;
        }

        public TrackSpendingViewModel2 GetSingle(Guid? guid)
        {
            var source = _accountBookRep.GetSingle(x => x.Id == guid);
            TrackSpendingViewModel2 resault = new TrackSpendingViewModel2
            {
                Id = source.Id,
                Categoryyy = source.Categoryyy,
                Amounttt = source.Amounttt,
                Dateee = source.Dateee,
                Remarkkk = source.Remarkkk,
            };
            return resault;
        }

        public void Add(AccountBook accountBook)
        {
            accountBook.Id = Guid.NewGuid();
            _accountBookRep.Create(accountBook);
        }

        public void Edit(TrackSpendingViewModel2 viewModel2)
        {
            var oldData = _accountBookRep.GetSingle(x => x.Id == viewModel2.Id);

            if (oldData != null)
            {
                oldData.Amounttt = viewModel2.Amounttt;
                oldData.Categoryyy = viewModel2.Categoryyy;
                oldData.Dateee = viewModel2.Dateee;
                oldData.Remarkkk = viewModel2.Remarkkk;
            }
        }

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}
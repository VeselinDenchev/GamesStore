namespace GamesStore.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Data;

    using Model;
    using Model.ViewModels;

    public class DiscountCodeService
    {
        private readonly GamesStoreDbContext dbContext;

        public DiscountCodeService(GamesStoreDbContext context)
        {
            dbContext = context;
        }

        public List<DiscountCodeViewModel> LoadAllDiscountCodes()
        {
            List<DiscountCodeViewModel> discountCodes = new List<DiscountCodeViewModel>();

            foreach (DiscountCode code in dbContext.DiscountCodes)
            {
                DiscountCodeViewModel codeViewModel = PassDataFromModelToViewModel(code);
                discountCodes.Add(codeViewModel);
            }

            return discountCodes;
        }

        public void CreateDiscountCode(DiscountCodeViewModel discountCode)
        {
            DiscountCode newDiscountCode = PassDataFromViewModelToModel(discountCode);

            dbContext.DiscountCodes.Add(newDiscountCode);
            dbContext.SaveChanges();
        }

        public DiscountCodeViewModel FindDiscountCodeById(string id)
        {
            DiscountCode code = dbContext.DiscountCodes.FirstOrDefault(m => m.Id == id);

            DiscountCodeViewModel codeViewModel = PassDataFromModelToViewModel(code);

            return codeViewModel;
        }

        public DiscountCodeViewModel CheckIfDiscountCodeIdIsValid(string id)
        {
            bool exists = CheckIfDiscountCodeExists(id);

            if (exists)
            {
                DiscountCodeViewModel discountCode = FindDiscountCodeById(id);
                return discountCode;
            }

            return null;
        }

        public void SaveEditedDiscountCode(DiscountCodeViewModel codeViewModel)
        {
            DiscountCode code = dbContext.DiscountCodes.FirstOrDefault(m => m.Id == codeViewModel.Id);

            bool hasDifferentCode = code.Code != codeViewModel.Code;
            bool hasDifferentDiscount = code.DiscountPercentage != codeViewModel.DiscountPercentage;
            bool hasBeenModified = hasDifferentCode || hasDifferentDiscount;

            if (hasDifferentCode)
            {
                code.Code = codeViewModel.Code;
            }
            if (hasDifferentDiscount)
            {
                code.DiscountPercentage = codeViewModel.DiscountPercentage;
            }
            if (hasBeenModified)
            {
                code.ModifiedAtUtc = DateTime.UtcNow;
                dbContext.SaveChanges();
            }
        }

        public void DeleteDiscountCode(string id)
        {
            DiscountCode codeToBeDeleted = new DiscountCode();
            codeToBeDeleted.Id = id;

            dbContext.DiscountCodes.Remove(codeToBeDeleted);
            dbContext.SaveChanges();
        }

        public bool CheckIfDiscountCodeExists(string id)
        {
            bool exists = dbContext.DiscountCodes.Any(m => m.Id == id);

            return exists;
        }

        private DiscountCodeViewModel PassDataFromModelToViewModel(DiscountCode model)
        {
            DiscountCodeViewModel viewModel = new DiscountCodeViewModel()
            {
                Id = model.Id,
                Code = model.Code.ToUpper(),
                DiscountPercentage = model.DiscountPercentage,
                CreatedAtUtc = model.CreatedAtUtc,
                ModifiedAtUtc = model.ModifiedAtUtc
            };

            return viewModel;
        }

        private DiscountCode PassDataFromViewModelToModel(DiscountCodeViewModel viewModel)
        {
            DiscountCode model = new DiscountCode()
            {
                Id = viewModel.Id,
                Code = viewModel.Code.ToUpper(),
                DiscountPercentage = viewModel.DiscountPercentage,
                CreatedAtUtc = viewModel.CreatedAtUtc,
                ModifiedAtUtc = viewModel.ModifiedAtUtc
            };

            return model;
        }
    }
}
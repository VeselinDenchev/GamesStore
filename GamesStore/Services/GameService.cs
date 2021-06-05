using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Data;

using Model;
using Model.ViewModels;

namespace GamesStore.Services
{
    public class GameService
    {
        private readonly GamesStoreDbContext dbContext;

        public GameService(GamesStoreDbContext context)
        {
            dbContext = context;
        }

        public List<GameViewModel> LoadAllGames()
        {
            List<GameViewModel> games = new List<GameViewModel>();

            foreach (Game game in dbContext.Games)
            {
                GameViewModel gameViewModel = PassDataFromModelToViewModel(game);
                games.Add(gameViewModel);
            }

            return games;
        }

        public void CreateGame(GameViewModel game)
        {
            Game newGame = PassDataFromViewModelToModel(game);

            dbContext.Games.Add(newGame);
            dbContext.SaveChanges();
        }

        public GameViewModel FindGameById(string id)
        {
            Game game = dbContext.Games.FirstOrDefault(m => m.Id == id);

            GameViewModel gameViewModel = PassDataFromModelToViewModel(game);

            return gameViewModel;
        }

        public GameViewModel CheckIfGameIdIsValid(string id)
        {
            bool exists = CheckIfGameExists(id);

            if (exists)
            {
                GameViewModel game = FindGameById(id);
                return game;
            }

            return null;
        }

        public void SaveEditedGame(GameViewModel gameViewModel)
        {
            Game game = dbContext.Games.FirstOrDefault(m => m.Id == gameViewModel.Id);

            bool hasDifferentName = game.Name != gameViewModel.Name;
            bool hasDifferentPlatform = game.Platform != gameViewModel.Platform;
            bool hasDifferentImgUrl = game.ImgUrl != gameViewModel.ImgUrl;
            bool hasDifferentDeveloper = game.Developer != gameViewModel.Developer;
            bool hasDifferentAgeRating = game.AgeRating != gameViewModel.AgeRating;
            bool hasDifferentDescription = game.Description != gameViewModel.Description;
            bool hasDifferentYearOfRelease = game.YearOfRelease != gameViewModel.YearOfRelease;
            bool hasDifferentPrice = game.Price != gameViewModel.Price;
            bool hasDifferentQuantity = game.Quantity != gameViewModel.Quantity;
            bool hasBeenModified = 
                hasDifferentName || hasDifferentPlatform || hasDifferentImgUrl || hasDifferentDeveloper || hasDifferentAgeRating 
                || hasDifferentDescription || hasDifferentYearOfRelease || hasDifferentPrice
                || hasDifferentQuantity;

            if (hasDifferentName)
            {
                game.Name = gameViewModel.Name;
            }
            if (hasDifferentPlatform)
            {
                game.Platform = gameViewModel.Platform;
            }
            if (hasDifferentImgUrl)
            {
                game.ImgUrl = gameViewModel.ImgUrl;
            }
            if (hasDifferentDeveloper)
            {
                game.Developer = gameViewModel.Developer;
            }
            if (hasDifferentAgeRating)
            {
                game.AgeRating = gameViewModel.AgeRating;
            }
            if (hasDifferentDescription)
            {
                game.Description = gameViewModel.Description;
            }
            if (hasDifferentYearOfRelease)
            {
                game.YearOfRelease = gameViewModel.YearOfRelease;
            }
            if (hasDifferentPrice)
            {
                game.Price = gameViewModel.Price;
            }
            if (hasDifferentQuantity)
            {
                game.Quantity = gameViewModel.Quantity;
            }
            if (hasBeenModified)
            {
                game.ModifiedAtUtc = DateTime.UtcNow;
                dbContext.SaveChanges();
            }
        }

        public void DeleteGame(GameViewModel game)
        {
            Game gameToBeDeleted = PassDataFromViewModelToModel(game);

            dbContext.Games.Remove(gameToBeDeleted);
            dbContext.SaveChanges();
        }

        public bool CheckIfGameExists(string id)
        {
            bool exists = dbContext.Games.Any(m => m.Id == id);

            return exists;
        }

        private GameViewModel PassDataFromModelToViewModel(Game model)
        {
            GameViewModel viewModel = new GameViewModel()
            {
                Id = model.Id,
                Name = model.Name,
                Platform = model.Platform,
                ImgUrl = model.ImgUrl,
                Developer = model.Developer,
                AgeRating = model.AgeRating,
                Description = model.Description,
                YearOfRelease = model.YearOfRelease,
                Price = model.Price,
                Quantity = model.Quantity,
                CreatedAtUtc = model.CreatedAtUtc,
                ModifiedAtUtc = model.ModifiedAtUtc
            };

            return viewModel;
        }

        private Game PassDataFromViewModelToModel(GameViewModel viewModel)
        {
            Game model = new Game()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Platform = viewModel.Platform,
                ImgUrl = viewModel.ImgUrl,
                Developer = viewModel.Developer,
                AgeRating = viewModel.AgeRating,
                Description = viewModel.Description,
                YearOfRelease = viewModel.YearOfRelease,
                Price = viewModel.Price,
                Quantity = viewModel.Quantity,
                CreatedAtUtc = viewModel.CreatedAtUtc,
                ModifiedAtUtc = viewModel.ModifiedAtUtc
            };

            return model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Commands;

public record CreatePlayCommand(string Name, int Lines, string Type) : ICommand<Result>;

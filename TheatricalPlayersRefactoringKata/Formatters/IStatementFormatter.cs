using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Formatters;

public interface IStatementFormatter
{
    string Format(Invoice invoice, Dictionary<string, Play> plays);
}

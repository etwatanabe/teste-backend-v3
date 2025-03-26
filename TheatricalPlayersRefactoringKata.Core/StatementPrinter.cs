using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using TheatricalPlayersRefactoringKata.Core.Calculators;
using TheatricalPlayersRefactoringKata.Core.Formatters;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.Core;

public class StatementPrinter
{
    private readonly IStatementFormatter _formatter;

    public StatementPrinter(IStatementFormatter formatter)
    {
        _formatter = formatter;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        return _formatter.Format(invoice, plays);
    }
}

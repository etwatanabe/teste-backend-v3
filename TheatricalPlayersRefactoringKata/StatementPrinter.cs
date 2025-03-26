using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using TheatricalPlayersRefactoringKata.Calculators;
using TheatricalPlayersRefactoringKata.Formatters;

namespace TheatricalPlayersRefactoringKata;

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

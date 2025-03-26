using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.Core.Formatters;

public interface IStatementFormatter
{
    string Format(Invoice invoice, Dictionary<Guid, Play> plays);
}

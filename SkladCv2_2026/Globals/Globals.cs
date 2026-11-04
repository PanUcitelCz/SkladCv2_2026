using DataEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SkladCv2_2026.Globals
{
    public static class Globals
    {

        // Jeden sdílený kontext pro celou aplikaci – vytváří se v App.OnStartup
        public static SkladContext context { get; set; } = null!;



        public static void Vratit()
        {
            // ToList(): během procházení měníme stav entit, kolekce Entries() se tím mění
            foreach (var entity in context.ChangeTracker.Entries().ToList())
            {
                switch (entity.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entity.Reload();
                        break;
                    case EntityState.Added:
                        entity.State = EntityState.Detached;
                        break;
                }
            }

        }


        public static bool IsValid(DependencyObject parent)
        {
            if (System.Windows.Controls.Validation.GetHasError(parent))
                return false;

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool HasUnsavedChanges()
        {
            return Globals.context.ChangeTracker.Entries().Any(e => e.State == EntityState.Added
                                                      || e.State == EntityState.Modified
                                                      || e.State == EntityState.Deleted);
        }

        /// <summary>
        /// Zeptá se uživatele na neuložené změny (volá se při zavírání okna).
        /// Vrací true, pokud se okno smí zavřít.
        /// </summary>
        public static bool PovolitZavreni()
        {
            if (!HasUnsavedChanges())
                return true;

            var result = MessageBox.Show("Máte neuložené změny. Chcete je uložit?", "Upozornění",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                return UlozitData(); // při chybě validace okno zůstane otevřené

            if (result == MessageBoxResult.No)
            {
                Vratit();
                return true;
            }

            return false; // Cancel
        }


        /// <summary>
        /// Zvaliduje změněné entity a uloží je do DB. Vrací true, pokud se uložení povedlo.
        /// </summary>
        public static bool UlozitData()
        {
            // 1. Najdeme všechny změněné nebo přidané entity
            var entitiesToValidate = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity)
                .ToList();

            var validationErrors = new List<ValidationResult>();

            // 2. Ručně spustíme validaci pro každou entitu
            foreach (var entity in entitiesToValidate)
            {
                var validationContext = new ValidationContext(entity);
                // Třetí parametr 'true' zajistí validaci všech vlastností
                Validator.TryValidateObject(entity, validationContext, validationErrors, validateAllProperties: true);
            }

            // 3. Pokud se našly chyby, zobrazíme je a neukládáme
            if (validationErrors.Any())
            {
                String text = "";
                foreach (var error in validationErrors)
                {
                    // error.MemberNames obsahuje názvy vlastností, kterých se chyba týká
                    string propertyName = error.MemberNames.Any() ? string.Join(", ", error.MemberNames) : "Neznámá vlastnost";

                    text += $"- Vlastnost: {propertyName}, Chyba: {error.ErrorMessage}" + Environment.NewLine;
                }

                MessageBox.Show("Entita obsahuje tyto chyby ověření:" + Environment.NewLine + text, "CHYBA VALIDACE", MessageBoxButton.OK, MessageBoxImage.Error);

                // Důležité: Nechceme pokračovat k SaveChanges()
                return false;
            }

            // 4. Pokud je vše v pořádku, pokusíme se uložit
            try
            {
                context.SaveChanges();
                return true;
            }
            // 5. Záznam mezitím změnil někdo jiný (RowVersion v DB už je jiná)
            catch (DbUpdateConcurrencyException)
            {
                MessageBox.Show("Záznam mezitím změnil nebo smazal někdo jiný.\nZavřete okno bez uložení a změnu proveďte znovu.", "KONFLIKT ZMĚN", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            // 6. Chyba na úrovni DB (constraint, cizí klíč, ...)
            catch (DbUpdateException e)
            {
                // Pro detailnější info se často hodí InnerException
                string errorDetails = e.InnerException?.Message ?? e.Message;
                MessageBox.Show("Nastala chyba při ukládání do databáze (např. porušení omezení).\n" + errorDetails, "CHYBA DATABÁZE", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nastala neočekávaná chyba při ukládání.\n" + ex.Message, "CHYBA", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
    }
}

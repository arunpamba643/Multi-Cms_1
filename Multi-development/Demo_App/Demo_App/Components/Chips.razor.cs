using AssociationEntities.CustomModels;
using Demo_App.Models;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Buttons;

namespace Demo_App.Components
{
    public partial class Chips : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string InputValue { get; set; }
        [Parameter]
        public EventCallback<string> OutputValueChanged { get; set; }
        [Parameter]
        public List<ChipItem> chipItems { get; set; }
        public string OutputValue { get; set; }
        public string newItem = string.Empty;
        protected override async Task OnParametersSetAsync()
        {
           
            if (!string.IsNullOrEmpty(InputValue))
            {
                List<string> chunks = InputValue.Split(";").ToList();
                chunks.ForEach(s => chipItems.Add(new ChipItem { Value = DateTime.Now.ToString(), Text = s }));
                chipItems.RemoveAll(s => string.IsNullOrEmpty(s.Text));
            }
        }

        public async Task DeleteChip(ChipDeletedEventArgs args)
        {
            var index = chipItems.FindIndex(x => x.Text == args.Text);
            if (index != -1)
            {
                chipItems.RemoveAt(index);
            }
            await onChipsChange();
        }

        public async Task onAddItem()
        {
            
            if (!string.IsNullOrEmpty(newItem))
            {
                chipItems.Add(new ChipItem { Value = DateTime.Now.ToString(), Text = newItem });
                chipItems = chipItems.Distinct().ToList(); // Remove duplicates
                chipItems.RemoveAll(s => string.IsNullOrEmpty(s.Text));
                newItem = string.Empty;
            }
            await onChipsChange();
        }

        public async Task onChipsChange()
        {
            OutputValue = string.Join(";", chipItems.Select(x => x.Text)); // Concatenate chip items
            await OutputValueChanged.InvokeAsync(OutputValue);
            chipItems.Clear(); // Clear chip items
        }

    }
}
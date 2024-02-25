using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using Valossy.Helpers.Validations;
using Valossy.Helpers.Validations.Attributes.Types;
using Valossy.Loggers;

namespace Valossy.Controls.AutoCompletes;

[GlobalClass]
[Tool]
public partial class AutoComplete : Control
{
    [Export] [NotNullValidation] public Array<string> ItemSource { get; set; }

    [Export] [NotNullValidation] public ItemList ItemList { get; set; }

    [Export] [NotNullValidation] public TextEdit AutocompleteTextBox { get; set; }

    [Export] [NotNullValidation] public PopupPanel PopupPanel { get; set; }

    [Export] [NotNullValidation] public int CharOffset { get; set; } = 10;

    public object SelectedItem { get; set; }

    public delegate void SelectedItemChangedEventHandler(object selectedItem);

    public event SelectedItemChangedEventHandler SelectedItemChanged;

    public Func<List<object>> ItemSourceProvider { get; set; }

    private readonly System.Collections.Generic.Dictionary<int, object> keyValuePairs = new System.Collections.Generic.Dictionary<int, object>();

    private int longestItemWidth = 0;

    public override void _Ready()
    {
        ValidationResult result = ValidationBuilder.Validate(this);

        if (result.Success == false)
        {
            Logger.Error(result.ValidationDescription);
            return;
        }

        this.AutocompleteTextBox.TextChanged += this.AutoCompleteTextChanged;
        this.ItemList.ItemSelected += this.ItemSelected;
        this.ToggleItemListVisibility();
    }

    private void ItemSelected(long index)
    {
        this.keyValuePairs.TryGetValue((int)index, out object selectedItem);
        this.SelectedItem = selectedItem;

        this.SelectedItemChanged?.Invoke(this.SelectedItem);
        this.ItemList.Clear();
        this.AutocompleteTextBox.Text = this.SelectedItem?.ToString();
        this.ToggleItemListVisibility();
    }

    private void AutoCompleteTextChanged()
    {
        this.PopulateItemList();
    }

    public void PopulateItemList()
    {
        this.ItemList.Clear();
        this.keyValuePairs.Clear();
        this.longestItemWidth = 0;

        if (string.IsNullOrWhiteSpace(this.AutocompleteTextBox.Text) == false)
        {
            foreach (object item in this.ItemSourceProvider != null ? this.ItemSourceProvider.Invoke() : this.ItemSource.Select(x => x as object))
            {
                string itemString = item.ToString();
                if (itemString?.ToLower().Contains(this.AutocompleteTextBox.Text?.ToLower()) == true)
                {
                    int index = this.ItemList.AddItem(itemString);
                    this.keyValuePairs[index] = item;

                    if(this.longestItemWidth < itemString.Length)
                    {
                        this.longestItemWidth = itemString.Length;
                    }
                }
            }
        }

        this.ToggleItemListVisibility();
    }

    public void ToggleItemListVisibility()
    {
        this.PopupPanel.Position = new Vector2I((int)AutocompleteTextBox.GlobalPosition.X,
            (int)AutocompleteTextBox.GlobalPosition.Y + (int)AutocompleteTextBox.Size.Y);
        
        this.PopupPanel.Visible = this.ItemList.ItemCount != 0;
        
        this.PopupPanel.Size = new Vector2I(this.longestItemWidth * this.CharOffset,
            (int)ItemList.Size.Y);

        // Does not work for some reason. Too early? too late?
        this.AutocompleteTextBox.CallDeferred(TextEdit.MethodName.GrabFocus);
        this.AutocompleteTextBox.GrabFocus();
    }
}
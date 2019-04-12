
using System.Collections.Generic;

public delegate void UpdateStackEvent();
public class ObservableStack<T> : Stack<T>
{
    private ObservableStack<Item> items;

    public ObservableStack() : base() { }
    public ObservableStack(ObservableStack<T> items) : base(items) { }


    public event UpdateStackEvent OnPush;
    public event UpdateStackEvent OnPop;
    public event UpdateStackEvent OnClear;

    public new void Push(T item)
    {
        base.Push(item);

        if (OnPush != null)
            OnPush();
    }

    public new T Pop()
    {
        T item = base.Pop();

        if (OnPop != null)
            OnPop();
        
        return item;
    }

    public new void Clear()
    {
        base.Clear();

        if (OnClear != null)
            OnClear();
    }
}

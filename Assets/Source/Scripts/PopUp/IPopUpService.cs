namespace Source.Scripts.PopUp
{
    public interface IPopUpService<in T>
    {
        public void ShowPopUp(T popUpContent);
    }
}
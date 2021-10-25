using UnityEngine;

public class Cell : MonoBehaviour
{
    private readonly Color _color1 = new Color(0.2f,0.3f,1f);
    private readonly Color _color2 = new Color(1f,0.3f,0.2f);
    private readonly Color _bgColor1 = new Color(0.75f,0.75f,0.75f);
    private readonly Color _bgColor2 = new Color(0.25f,0.25f,0.25f);
    private readonly Color _mineFlipColor= new Color(0.5f,0.5f,0.5f);

    private Color _mineColor;

    public void SetColors(bool even, bool isMine)
    {
        GetComponent<SpriteRenderer>().color = even ? _bgColor1 : _bgColor2;
        _mineColor = isMine ? _color2 : _color1;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = _mineColor;
    }

    private void FlipColor()
    {
        SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sr.color = sr.color == _mineColor ? _mineFlipColor : _mineColor;
    }
    
    public void ChangeColor(bool hide)
    {
        SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sr.color = hide ? _mineFlipColor : _mineColor;
    }

    private void OnMouseDown()
    {
        if(GameController.GameInputEnabled)
        {
            FlipColor();
            GetComponentInParent<MineField>().DetonateCell(_mineColor);
        }
    }
}

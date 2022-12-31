using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _equipWeaponPoint;

    private Weapon _currentWeapon;
    private Weapon _previousWeapon;
    private int _currentWeaponNumber = 0;
    private int _currentHealth;

    public int Money { get; private set; }

    private void Start()
    {
        _currentHealth = _health;
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            Destroy(_currentWeapon.gameObject);
        }
    }

    public void AddMoney(int reward)
    {
        Money += reward;
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        CleanClonWeapon();

        if (_currentWeaponNumber == _weapons.Count - 1)
        {
            _currentWeaponNumber = 0;
        }
        else
        {
            _currentWeaponNumber++;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        CleanClonWeapon();

        if (_currentWeaponNumber == 0)
        {
            _currentWeaponNumber = _weapons.Count - 1;
        }
        else
        {
            _currentWeaponNumber--;
        }

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _currentWeapon=Instantiate(_currentWeapon, _equipWeaponPoint.position, Quaternion.identity);
    }

    private void CleanClonWeapon()
    {
        _previousWeapon = _currentWeapon;
        Destroy(_previousWeapon.gameObject);
    }
}

using System;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Array atau List untuk menyimpan referensi ke semua musuh
    public GameObject[] enemies;

    void Start()
    {
        // Dapatkan referensi ke semua musuh pada awal permainan
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Logika umum untuk semua musuh
    public void EnemyDestroyed(GameObject destroyedEnemy)
    {
        // Logika yang harus dijalankan ketika musuh dihancurkan
        Debug.Log("Enemy Destroyed!");

        // Implementasikan logika lain yang ingin Anda eksekusi saat musuh dihancurkan

        // Contoh: Hapus musuh dari array atau list
        enemies = Array.FindAll(enemies, enemy => enemy != destroyedEnemy);
    }
}
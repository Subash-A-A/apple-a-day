using UnityEditor;

[CustomEditor(typeof(Weapon))]
public class WeaponEditor : Editor
{
    #region SerializedProperties
    SerializedProperty isMeeleWeapon;
    SerializedProperty meeleDashPower;
    SerializedProperty meeleDashTime;
    SerializedProperty meeleDashCooldown;
    SerializedProperty tr;
    SerializedProperty meeleHitBox;
    SerializedProperty player;
    SerializedProperty attackPoint;
    SerializedProperty bulletPrefab;
    SerializedProperty impactForce;
    SerializedProperty bulletSpeed;
    SerializedProperty bulletDamage;
    SerializedProperty delayBetweenShots;
    SerializedProperty range;
    SerializedProperty targetLayer;
    SerializedProperty isFullAuto;
    SerializedProperty useTorch;
    SerializedProperty torchLight;
    #endregion

    private void OnEnable()
    {
        isMeeleWeapon = serializedObject.FindProperty("isMeeleWeapon");
        meeleDashPower = serializedObject.FindProperty("meeleDashPower");
        meeleDashTime = serializedObject.FindProperty("meeleDashTime");
        meeleDashCooldown = serializedObject.FindProperty("meeleDashCooldown");
        tr = serializedObject.FindProperty("tr");
        meeleHitBox = serializedObject.FindProperty("meeleHitBox");
        
        player = serializedObject.FindProperty("player");
        attackPoint = serializedObject.FindProperty("attackPoint");

        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
        impactForce = serializedObject.FindProperty("impactForce");
        bulletSpeed = serializedObject.FindProperty("bulletSpeed");
        bulletDamage = serializedObject.FindProperty("bulletDamage");
        delayBetweenShots = serializedObject.FindProperty("delayBetweenShots");
        range = serializedObject.FindProperty("range");
        targetLayer = serializedObject.FindProperty("targetLayer");
        isFullAuto = serializedObject.FindProperty("isFullAuto");
        useTorch = serializedObject.FindProperty("useTorch");
        torchLight = serializedObject.FindProperty("torchLight");
    }

    public override void OnInspectorGUI()
    {
        Weapon weaponScript = (Weapon)target;
        serializedObject.Update();

        EditorGUILayout.PropertyField(player);
        EditorGUILayout.PropertyField(attackPoint);
        EditorGUILayout.PropertyField(isMeeleWeapon);
        EditorGUILayout.PropertyField(useTorch);
        EditorGUILayout.PropertyField(torchLight);

        if (weaponScript.isMeeleWeapon)
        {
            EditorGUILayout.PropertyField(meeleDashPower);
            EditorGUILayout.PropertyField(meeleDashTime);
            EditorGUILayout.PropertyField(meeleDashCooldown);
            EditorGUILayout.PropertyField(tr);
            EditorGUILayout.PropertyField(meeleHitBox);
        }
        else
        {
            EditorGUILayout.PropertyField(bulletPrefab);
            EditorGUILayout.PropertyField(impactForce);
            EditorGUILayout.PropertyField(bulletSpeed);
            EditorGUILayout.PropertyField(bulletDamage);
            EditorGUILayout.PropertyField(delayBetweenShots);
            EditorGUILayout.PropertyField(range);
            EditorGUILayout.PropertyField(targetLayer);
            EditorGUILayout.PropertyField(isFullAuto);
        }

        serializedObject.ApplyModifiedProperties();
    }
}

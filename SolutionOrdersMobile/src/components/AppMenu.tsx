import React, { useRef, useState } from 'react';
import { Animated, Pressable, StyleSheet, Text, View } from 'react-native';
import { Screen } from '../types/screen';

interface AppMenuProps {
  currentScreen: Screen;
  onChangeScreen: (screen: Screen) => void;
}

const menuItems: { label: string; icon: string; screen: Screen }[] = [
  { label: 'Workouts', icon: '🏋️', screen: 'workouts' },
  { label: 'Exercises', icon: '💪', screen: 'exercises' },
  { label: 'Progress', icon: '📈', screen: 'progress' },
  { label: 'Categories', icon: '📂', screen: 'categories' },
  { label: 'Equipment', icon: '🎒', screen: 'equipment' },
  { label: 'Goals', icon: '🎯', screen: 'goals' },
  { label: 'Users', icon: '👤', screen: 'users' },
  { label: 'About', icon: 'ℹ️', screen: 'about' },
];

const AppMenu: React.FC<AppMenuProps> = ({ currentScreen, onChangeScreen }) => {
  const [isOpen, setIsOpen] = useState(false);
  const slideAnim = useRef(new Animated.Value(-260)).current;

  const handleSelect = (screen: Screen) => {
    onChangeScreen(screen);
    closeMenu();
  };

  const openMenu = () => {
    setIsOpen(true);

    Animated.timing(slideAnim, {
      toValue: 0,
      duration: 250,
      useNativeDriver: true,
    }).start();
  };

  const closeMenu = () => {
    Animated.timing(slideAnim, {
      toValue: -260,
      duration: 250,
      useNativeDriver: true,
    }).start(() => {
      setIsOpen(false);
    });
  };

  return (
    <>
      <View style={styles.topBar}>
        <Pressable style={styles.hamburgerButton} onPress={openMenu}>
          <Text style={styles.hamburgerText}>☰</Text>
        </Pressable>

        <Text style={styles.appTitle}>Workout Planner</Text>
      </View>

      {isOpen ? (
        <View style={styles.overlay}>
          <Pressable style={styles.backdrop} onPress={closeMenu} />

          <Animated.View
            style={[
              styles.drawer,
              {
                transform: [{ translateX: slideAnim }],
              },
            ]}
          >
            <Text style={styles.drawerTitle}>Menu</Text>

            {menuItems.map(item => {
              const active = item.screen === currentScreen;

              return (
                <Pressable
                  key={item.screen}
                  style={[styles.menuItem, active && styles.activeMenuItem]}
                  onPress={() => handleSelect(item.screen)}
                >
                  <Text
                    style={[
                      styles.menuItemText,
                      active && styles.activeMenuItemText,
                    ]}
                  >
                    {item.icon} {item.label}
                  </Text>
                </Pressable>
              );
            })}
          </Animated.View>
        </View>
      ) : null}
    </>
  );
};

const styles = StyleSheet.create({
  topBar: {
    height: 72,
    paddingTop: 32,
    paddingHorizontal: 20,
    backgroundColor: '#fbf7f7',
    flexDirection: 'row',
    alignItems: 'center',
    gap: 14,
  },
  hamburgerButton: {
    width: 42,
    height: 42,
    alignItems: 'center',
    justifyContent: 'center',
  },
  hamburgerText: {
    color: '#5d3ea8',
    fontSize: 24,
    fontWeight: '800',
    marginTop: -2,
  },
  appTitle: {
    fontSize: 20,
    fontWeight: '800',
    color: '#322056',
  },
  overlay: {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    zIndex: 9999,
    elevation: 9999,
    flexDirection: 'row',
  },
  backdrop: {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: 'rgba(0,0,0,0.25)',
  },
  drawer: {
    width: 260,
    height: '100%',
    backgroundColor: '#fbf7f7',
    paddingTop: 56,
    paddingHorizontal: 20,
    borderTopRightRadius: 24,
    borderBottomRightRadius: 24,
    zIndex: 10000,
    elevation: 10000,
  },
  drawerTitle: {
    fontSize: 26,
    fontWeight: '800',
    color: '#322056',
    marginBottom: 20,
  },
  menuItem: {
    paddingVertical: 14,
    paddingHorizontal: 14,
    borderRadius: 14,
    marginBottom: 8,
    backgroundColor: '#eee3ff',
  },
  activeMenuItem: {
    backgroundColor: '#5d3ea8',
  },
  menuItemText: {
    fontSize: 16,
    fontWeight: '700',
    color: '#5a359d',
  },
  activeMenuItemText: {
    color: '#fff',
  },
});

export default AppMenu;

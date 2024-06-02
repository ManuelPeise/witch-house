import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import common_en from './resources/en/common.en.json';
import menu_en from './resources/en/menu.en.json';
import administration_en from './resources/en/administration.en.json';
import common_de from './resources/de/common.de.json';
import menu_de from './resources/de/menu.de.json';
import administration_de from './resources/de/administration.de.json';

const resources = {
  en: {
    common: common_en,
    menu: menu_en,
    administration: administration_en,
  },
  de: {
    common: common_de,
    menu: menu_de,
    administration: administration_de,
  },
};

i18n.use(initReactI18next).init({
  resources: resources,
  lng: 'en',
});

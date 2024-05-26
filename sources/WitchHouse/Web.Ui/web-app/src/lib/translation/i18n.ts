import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import common_en from './resources/en/common.en.json';
import common_de from './resources/de/common.de.json';

const resources = {
  en: {
    common: common_en,
  },
  de: {
    common: common_de,
  },
};

i18n.use(initReactI18next).init({
  resources: resources,
  lng: 'en',
});

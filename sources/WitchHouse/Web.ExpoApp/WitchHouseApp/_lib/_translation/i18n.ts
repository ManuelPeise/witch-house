import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import common_en from './_resources/en/common.en.json';
import common_de from './_resources/de/common.de.json';

const resources = {
  de: {
    common: common_de,
  },
  en: {
    common: common_en,
  },
};

i18n.use(initReactI18next).init({
  compatibilityJSON: 'v3',
  resources: resources,
  lng: 'en',
});

export default i18n;

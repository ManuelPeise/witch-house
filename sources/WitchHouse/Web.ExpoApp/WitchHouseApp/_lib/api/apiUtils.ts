import { endPoints } from './apiConfiguration';
import axiosClient from './axiosClient';

export const checkApiIsAvailable = async (callback: (state: boolean) => void) => {
  await axiosClient.get(endPoints.health.apiIsAvailable).then(async (res) => {
    if (res.status === 200) {
      const isAvailable: boolean = res.data;
      callback(isAvailable);

      return;
    }

    callback(false);
  });
};

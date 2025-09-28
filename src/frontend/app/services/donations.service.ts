import axios, { type AxiosResponse } from "axios"
import type { Donation } from "~/models/spinning-wheel.models"

export const getDonations = (): Promise<AxiosResponse<Donation[]>> => {
    return axios.get<Donation[]>(`${import.meta.env.VITE_API_URL}/api/donations`);
}
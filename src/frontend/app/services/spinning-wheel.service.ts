import axios, { type AxiosResponse } from "axios"
import type { WheelParticipant } from "~/models/spinning-wheel.models"

export const getParticipants = (): Promise<AxiosResponse<WheelParticipant[]>> => {
    return axios.get<WheelParticipant[]>(`${import.meta.env.VITE_API_URL}/api/wheel-participants`);
}
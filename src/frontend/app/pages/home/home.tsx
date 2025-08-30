import { SpinningWheel } from "~/components/spinning-wheel/spinning-wheel";
import type { WheelParticipant } from "~/models/spinning-wheel.models";

export default function Home() {
  const wheelParticipants: WheelParticipant[] = [
    {
      name: "1",
      value: 1
    },
    {
      name: "2",
      value: 100
    },
    {
      name: "3",
      value: 100
    }
  ]

  return <div className="flex mx-auto grow flex-col justify-center"><SpinningWheel participants={wheelParticipants} /></div>;
}

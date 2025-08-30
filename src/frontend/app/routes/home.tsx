import type { Route } from "./+types/home";
import { Welcome } from "../welcome/welcome";
import { SpinningWheel } from "~/components/SpinningWheel/SpinningWheel";
import type { WheelParticipant } from "~/models/SpinningWheel.models";

export function meta({ }: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

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

  return <SpinningWheel participants={wheelParticipants} />;
}

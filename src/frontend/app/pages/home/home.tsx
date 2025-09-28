import { SpinningWheel } from "~/components/spinning-wheel/spinning-wheel";
import { AgGridReact } from 'ag-grid-react';
import { useEffect, useState } from "react";
import type { Donation, WheelParticipant } from "~/models/spinning-wheel.models";
import type { ColDef } from "ag-grid-community";
import * as signalR from '@microsoft/signalr';
import { getParticipants } from '~/services/spinning-wheel.service';
import { getDonations } from "~/services/donations.service";


export default function Home() {

  const [donations, setDonations] = useState<Donation[]>([
    { displayName: "Tesla", message: "Model Y", amount: 64951, createdAt: "Segodn9" },
    { displayName: "Tesla", message: "Model Y", amount: 64950, createdAt: "Segodn9" }
  ]);

  const [colDefs, setColDefs] = useState<ColDef<Donation>[]>([
    { field: "displayName" },
    { field: "message" },
    { field: "amount" },
    { field: "createdAt" }
  ]);

  useEffect(() => {
    getDonations().then(response => setDonations(response.data));
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`${import.meta.env.VITE_API_URL}/donationHub`)
      .build();

    connection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    connection.on('ParticipantAdded', (newParticipant: Donation) => {
      setDonations(oldParticipants => [...oldParticipants, newParticipant]);
    });
  }, [])

  return <div className="flex mx-auto grow flex-row justify-center">
    <div style={{ width: 500 }}>
      <AgGridReact
        rowData={donations}
        columnDefs={colDefs}
      />
    </div>
    <SpinningWheel participants={donations.map<WheelParticipant>(donation => { return { name: donation.message, value: donation.amount } })} />
    <div style={{ width: 500 }}>
      <AgGridReact
        rowData={donations}
        columnDefs={colDefs}
      />
    </div>
  </div>;
}

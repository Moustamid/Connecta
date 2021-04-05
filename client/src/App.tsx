import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from 'axios';
//-sementic ui :
import 'semantic-ui-css/semantic.min.css';
import { Header, List } from 'semantic-ui-react';

const App = () => {
  // hooks :
  const [activities, setActivities] = useState([]);

  useEffect(() => {
    // effect;
    axios.get('http://localhost:5000/api/activities').then((response) => {
      setActivities(response.data);
      console.log(response);
    });

    return () => {
      // cleanup;
    };
  }, []);

  return (
    <div>
      <Header as="h2" icon="users" content="Connecta" />

      <List>
        {activities.map((activity: any) => (
          <List.Item key={activity.id}>{activity.title}</List.Item>
        ))}
      </List>
    </div>
  );
};

export default App;

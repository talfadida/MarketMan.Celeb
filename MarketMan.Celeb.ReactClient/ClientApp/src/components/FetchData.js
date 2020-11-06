import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;
  
  constructor(props) {
    super(props);
      this.state = { celebData: [], loading: true };
      this.api = "http://localhost:57707/api/main";
  }

  componentDidMount() {
    this.populateCelebData();
    }


    async deleteMe(key) {
        console.log('deleting #' + key);
        this.setState({ loading: true });
        await fetch(this.api + '/' + key, {
            method: 'DELETE'            
        });
        this.setState({ loading: false, celebData: this.state.celebData.filter(p => p.key != key) });
        this.render();
    }

    async resetCelebData() {
        console.log('reseting.. ');
        this.setState({ loading: true });
        const response = await fetch(this.api, {
            method: 'POST'
        });
        this.populateCelebData();
        this.render();
    }

    
   renderCelebTable(celebs) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Picture</th>
            <th>Name</th>
            <th>BirthDate</th>
            <th>Role</th>
            <th>Gender</th>
          </tr>
        </thead>
        <tbody>
          {celebs.map(celeb =>
              <tr key={celeb.Key}>
                  <td><img alt={celeb.name} src={celeb.imageUrl} /><br /><button onClick={() =>  this.deleteMe(celeb.key)}>Delete</button></td>
                  <td>#{celeb.key} -> {celeb.name}<br /></td> 
                  <td>{celeb.birthDate}</td>
                  <td>{celeb.role}</td>
                  <td>{celeb.gender}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() { 
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : this.renderCelebTable(this.state.celebData);

    return (
        <div>
            <h1 id="tabelLabel" >Celebrities from IMDB</h1> <button onClick={()=> this.resetCelebData() }>Reset Data</button>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
    }


  async populateCelebData() {
      const response = await fetch(this.api);
      const data = await response.json();
      this.setState({ celebData: data, loading: false });
  }
}

import { createClaimsTable } from '../../utils/claimUtils';

import '../../styles/sample-App.css';

export const IdTokenData = (props) => {
    const tokenClaims = createClaimsTable(props.idTokenClaims);

    const tableRow = Object.keys(tokenClaims).map((key, index) => {
        return (
            <tr key={key}>
                {tokenClaims[key].map((claimItem) => (
                    <td key={claimItem}>{claimItem}</td>
                ))}
            </tr>
        );
    });
    return (
        <>
            <div className="data-area-div">
                <p>
                    See below the claims in your <strong> ID token </strong>. For more information, visit:{' '}
                    <span>
                        <a href="https://docs.microsoft.com/en-us/azure/active-directory/develop/id-tokens#claims-in-an-id-token">
                            docs.microsoft.com
                        </a>
                    </span>
                </p>
                <div className="data-area-div">
                    <table style={{ width: '100%', borderCollapse: 'collapse', border: '1px solid #ddd' }}>
                        <thead>
                            <tr style={{ backgroundColor: '#f2f2f2' }}>
                                <th style={{ border: '1px solid #ddd', padding: '8px' }}>Claim</th>
                                <th style={{ border: '1px solid #ddd', padding: '8px' }}>Value</th>
                                <th style={{ border: '1px solid #ddd', padding: '8px' }}>Description</th>
                            </tr>
                        </thead>
                        <tbody>{tableRow}</tbody>
                    </table>
                </div>
            </div>
        </>
    );
};